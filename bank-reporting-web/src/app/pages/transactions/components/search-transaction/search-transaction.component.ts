import { Component, EventEmitter, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { SearchTransactionModel } from '../../../../models/transaction/search-transaction-model';
import { TransactionService } from '../../../../services/transaction.service';
import { FormGroup, UntypedFormBuilder } from '@angular/forms';
import { Subscription } from 'rxjs';
import { ScreenService } from '../../../../services/screen.service';

@Component({
  selector: 'app-search-transaction',
  standalone: false,
  templateUrl: './search-transaction.component.html',
  styleUrl: './search-transaction.component.scss'
})
export class SearchTransactionComponent implements OnInit, OnDestroy {

  // Form
  public form!: FormGroup;

  // Subscription
  subSrcModel: Subscription | null = null;
  subIsSmallScreen: Subscription | null = null;

  // Search model
  public srcModel: SearchTransactionModel | null = null;

  // Outputs
  @Output() public search = new EventEmitter<SearchTransactionModel>();
  @Output() public csv = new EventEmitter<SearchTransactionModel>();
  @Output() public import = new EventEmitter<FormData>();

  public showSpinner = false;
  public isSmallScreen = false;

  @ViewChild('fileInput', { static: false }) fileInput: any;

  constructor(private fb: UntypedFormBuilder,
    private srv: TransactionService,
    private screenSrv: ScreenService
  ) {

  }

  ngOnInit(): void {
    // Get search
    this.subSrcModel = this.srv.srcModel
      .subscribe(x => {
        this.srcModel =  x
      });

    // Create form
    this.createForm();

    this.screenSrv.isSmallScreen.subscribe(x => {
      this.isSmallScreen = x;
    });
  }

  ngOnDestroy(): void {
    // Free
    this.subSrcModel?.unsubscribe();
    this.subIsSmallScreen?.unsubscribe();
  }

  private createForm() {
    // Create form
    this.form = this.fb.group({
      idPartner: [null],
      idMerchant: [null],
      createDateFrom: [null],
      createDateTo: [null],
      idDirection: [null],
      amountFrom: [null],
      amountTo: [null],
      idCcy: [null],
      debtorIban: [null],
      beneficiaryIban: [null],
      idStatus: [null],
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
    }

    return '';
  }

  //#endregion

  public onSearch(): void {
    this.search.emit(this.form.value);
  }

  public onCsv(): void {
    this.csv.emit(this.form.value);
  }

  public onBrowseFiles() {

  }

  public importFile() {

  }

  public onImport(): void {
    this.import.emit(this.form.value);
  }

  public onReset(): void {
    this.form?.reset();
    //this.srv?.srcModel.next(null);
    this.srv?.srcRes.next(null);
  }
}

