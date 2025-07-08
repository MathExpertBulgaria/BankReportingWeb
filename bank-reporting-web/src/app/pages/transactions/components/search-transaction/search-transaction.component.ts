import { Component, EventEmitter, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { SearchTransactionModel } from '../../models/search-transaction-model';
import { TransactionService } from '../../../../services/transaction.service';
import { AbstractControl, FormGroup, UntypedFormBuilder, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { ScreenService } from '../../../../services/screen.service';
import { PartnerModel } from '../../../partners/models/partner-model';
import { PartnerService } from '../../../../services/partner.service';
import { MerchantService } from '../../../../services/merchant.service';
import { SearchPartnerModel } from '../../../partners/models/search-partner-model';
import { MerchantModel } from '../../../merchants/models/merchant-model';
import { SearchMerchantModel } from '../../../merchants/models/search-merchant-model';

@Component({
  selector: 'app-search-transaction',
  standalone: false,
  templateUrl: './search-transaction.component.html',
  styleUrl: './search-transaction.component.scss'
})
export class SearchTransactionComponent implements OnInit, OnDestroy {

  // Form
  public form!: FormGroup;
  // Locals
  public showSpinner = false;
  public isSmallScreen = false;
  public partners: PartnerModel[] | null = null;
  public merchants: MerchantModel[] | null = null;
  public isPartnerRequired = false;
  public isMerchantRequired = false;
  
  // Subscription
  subSrcModel: Subscription | null = null;
  subIsSmallScreen: Subscription | null = null;
  subPartners: Subscription | null = null;
  subMerchants: Subscription | null = null;
  subPartner: Subscription | null = null;
  subFormChanges: Subscription | null = null;

  // Search model
  public srcModel: SearchTransactionModel | null = null;

  // Outputs
  @Output() public search = new EventEmitter<SearchTransactionModel>();
  @Output() public csv = new EventEmitter<SearchTransactionModel>();
  @Output() public import = new EventEmitter<FormData>();

  @ViewChild('fileInput', { static: false }) fileInput: any;

  constructor(private fb: UntypedFormBuilder,
    private srv: TransactionService,
    private partnerSrv: PartnerService,
    private merchantSrv: MerchantService,
    private screenSrv: ScreenService
  ) {

  }

  ngOnInit(): void {
    // Get search
    this.subSrcModel = this.srv.srcModel.subscribe(x => {
      this.srcModel = x;
    });

    // Create form
    this.createForm();

    // Get search
    this.subFormChanges = this.form.valueChanges.subscribe(x => {
      this.srv.srcFormModel.next(x);
    });

    // Screen
    this.subIsSmallScreen = this.screenSrv.isSmallScreen.subscribe(x => {
      this.isSmallScreen = x;
    });

    // Partner
    this.subPartner = this.form.controls['idPartner'].valueChanges.subscribe(x => {
      if (x > 0) {
        this.form.controls['merchantName'].enable();
      } else {
        this.form.controls['merchantName'].disable();
      }
    });
  }

  ngOnDestroy(): void {
    // Free
    this.subSrcModel?.unsubscribe();
    this.subIsSmallScreen?.unsubscribe();
    this.subPartners?.unsubscribe();
    this.subMerchants?.unsubscribe();
    this.subPartner?.unsubscribe();
    this.subFormChanges?.unsubscribe();
  }

  private createForm() {
    // Create form
    this.form = this.fb.group({
      idPartner: [null],
      partnerName: [null, this.validatePartnerNotFound()],
      idMerchant: [null],
      merchantName: [{ value: null, disabled: true, }, this.validateMerchantNotFound()],
      createDateFrom: [null],
      createDateTo: [null],
      idDirection: [null],
      amountFrom: [null],
      amountTo: [null],
      idCcy: [null],
      debtorIban: [null],
      beneficiaryIban: [null],
      status: [null],
      externalId: [null],
    });
  }

  //#region Validation

  formNotValid(name: string): boolean {
    const field = this.form.get(name);
    
    if (field !== null) {
      return field.invalid && field.touched;
    }

    return false;
  }

  formErrorType(name: string): string {
    const field = this.form.get(name);
    
    if (field === null) {
      return '';
    }

    // Check
    if (field.hasError('required')) {
      return 'required';
    } else if (field.hasError('matDatepickerParse')) {
      return 'date format';
    } else if (field.hasError('notFound')) {
      return 'not found';
    }

    return '';
  }

  validate() {
    this.form.markAllAsTouched();
    this.form.updateValueAndValidity();
    if (!this.form.valid) {
      return false;
    }

    return true;
  }

  validatePartnerNotFound(): Validators {
    return (control: AbstractControl): {[key: string]: any} | null => {
      if (!this.form) {
        return null;
      }

      const idPartner = this.form.controls['idPartner'].value;
      const isValid = !idPartner || (this.partners && this.partners.filter(x => x.id == idPartner).length > 0);

      if (!isValid) {
        return {'notFound': true } 
      }

      return null;
    };
  }

  validateMerchantNotFound(): Validators {
    return (control: AbstractControl): {[key: string]: any} | null => {
      if (!this.form) {
        return null;
      }

      const idMerchant = this.form.controls['idMerchant'].value;
      const isValid = !idMerchant || (this.merchants && this.merchants.filter(x => x.id == idMerchant).length > 0);

      if (!isValid) {
        return {'notFound': true } 
      }

      return null;
    };
  }

  setImportReqirements() {
    this.isPartnerRequired = true;
    this.isMerchantRequired = true;

    this.form.controls['idPartner'].addValidators(Validators.required);
    this.form.controls['idMerchant'].addValidators(Validators.required);

    this.form.controls['idPartner'].updateValueAndValidity();
    this.form.controls['idMerchant'].updateValueAndValidity();
  }

  //#endregion

  //#region Filter partner

  public onPartnerSearch(val: string) {
    // Check
    if (val.length < 3) {
      // return
      return;
    }

    // Model
    const model = <SearchPartnerModel> { name: val };

    // Call server
    this.subPartners = this.partnerSrv.search(model)
    .subscribe({
      next: (res: any) => {
        // Set data
        if (res.data) {
          // Set
          this.partners = res.data.res;

          // Clear 
          this.clearMerchant();
        } else {
          // Error
        }
      },
      error: (err: any) => {
        // Error
      }
    });
  }

  public onPartnerSelected(val: number) {
    const selection = this.partners?.filter(x => x.id == val)[0]!;

    this.form.controls['idPartner'].setValue(val);
    this.form.controls['partnerName'].setValue(selection.name);

    // Clear
    this.clearMerchant();
  }

  private clearMerchant() {
    this.form.controls['idMerchant'].setValue(null);
    this.form.controls['merchantName'].setValue(null);
    this.merchants = null;
  }
  
  //#endregion

  //#region Filter merchant

  public onMerchantSearch(val: string) {
    // Check
    if (val.length < 3) {
      // return
      return;
    }

    // Id partner
    const idPartner = this.form.controls['idPartner'].value;
    // Model
    const model = <SearchMerchantModel> { idPartner: idPartner,  name: val };

    // Call server
    this.subMerchants = this.merchantSrv.search(model)
    .subscribe({
      next: (res: any) => {
        // Set data
        if (res.data) {
          // Set
          this.merchants = res.data.res;
        } else {
          this.form.controls['idMerchant'].setValue(null);
        }
      },
      error: (err: any) => {
        // Error
      }
    });
  }

  public onMerchantSelected(val: number) {
    const selection = this.merchants?.filter(x => x.id == val)[0]!;

    this.form.controls['idMerchant'].setValue(val);
    this.form.controls['merchantName'].setValue(selection.name);
  }
  
  //#endregion

  public onSearch(): void {
    // Validate
    if (!this.validate()) {
      // return
      return;
    }
    
    this.search.emit(this.form.value);
  }

  public onCsv(): void {
    this.csv.emit(this.form.value);
  }

  public onBrowseFiles() {
    // Requirements
    this.setImportReqirements();

    // Validate
    if (!this.validate()) {
      // return
      return;
    }

    this.fileInput.nativeElement.click();
  }

  public importFile() {
    const model = new FormData();
    const src = this.form.value as SearchTransactionModel;
    model.append('search', JSON.stringify(src));
    model.append(`file`, this.fileInput.nativeElement.files[0]);
    
    this.import.next(model);

    // Clear
    this.fileInput.nativeElement.value = null;
  }

  public onImport(): void {
    this.import.emit(this.form.value);
  }

  public onReset(): void {
    this.form?.reset();

    this.partners = null;
    this.merchants = null;

    this.isPartnerRequired = false;
    this.isMerchantRequired = false;

    this.srv?.srcRes.next(null);
    this.srv?.showRes.next(false);
  }
}

