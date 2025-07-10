import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { createDORfromError, createDORfromObj, OperationAlert } from '../../models/data-operation-result-model';
import { SearchTransactionModel } from './models/search-transaction-model';
import { finalize, Subscription } from 'rxjs';
import { TransactionService } from '../../services/transaction.service';
import { ResModel } from '../../models/res-model';
import { TransactionModel } from './models/transaction-model';
import { AppService } from '../../services/app.service';
import { DownloadFileModel } from '../../models/download-file.model';
import { downloadFile } from '../../functions/download-file';
import { SortModel } from '../../models/sort-model';
import { PageModel } from '../../models/page-model';
import { InitialPage } from '../../consts/page-const.model';
import { TransactionTableComponent } from './components/transaction-table/transaction-table.component';

@Component({
  selector: 'app-transactions',
  standalone: false,
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.scss'
})
export class TransactionsComponent implements OnInit, OnDestroy {

  public oprRes: OperationAlert[] | null = null;
  public showSpinner = false;
  public isExpanded = true;
  public showTable = false;
  public pageData?: PageModel;
  public sortData?: SortModel |null = null;

  // Subscriptions
  subGet: Subscription | null = null;
  subSearch: Subscription | null = null;
  subShowRes: Subscription | null = null;
  subCsv: Subscription | null = null;
  subImport: Subscription | null = null;
  subPageData: Subscription | null = null;
  subSort: Subscription | null = null;

  @ViewChild(TransactionTableComponent, { static: false }) table: TransactionTableComponent | null = null;

  constructor(private srv: TransactionService,
    private appSrv: AppService
  ) {

  }

  ngOnInit(): void {
    this.showSpinner = true;
    
    // Call server
    this.subGet = this.srv.get().pipe(
      finalize(() => {
        this.showSpinner = false;
      })
    )
    .subscribe({
      next: (res: any) => {
        // Set data
        if (res.data) {
          // Set
          this.srv.srcModel.next(res.data);
          this.srv.srcModelInit = res.data;
        } else {
          this.oprRes = createDORfromObj<SearchTransactionModel>(res).getAlerts();
        }
      },
      error: (err: any) => {
        this.oprRes = createDORfromError<SearchTransactionModel>(err).getAlerts();
      }
    });

    // Show res
    this.subShowRes = this.srv.showRes.subscribe(x => {
      this.showTable = x;
    });

    // Page data
    this.subPageData = this.srv.pageData.subscribe(x => {
      this.pageData = x;
    });

    // Sort data
    this.subSort = this.srv.sortData.subscribe(x => {
      this.sortData = x;
    });
  }

  ngOnDestroy(): void {
    // Free
    this.subGet?.unsubscribe();
    this.subSearch?.unsubscribe();
    this.subShowRes?.unsubscribe();
    this.subCsv?.unsubscribe();
    this.subImport?.unsubscribe();
    this.subPageData?.unsubscribe();
    this.subSort?.unsubscribe();

    // Clear
    //this.srv.srcModel.next(null);
    //this.srv.srcRes.next(null);
    //this.srv.showRes.next(false);
  }

  onSearch(event: any) {
    const model = event as SearchTransactionModel;

    // Store
    model.nomen = this.srv.srcModel.getValue()?.nomen;
    this.srv.srcModel.next(model);

    // Set
    if (!model.isPaging) {
      this.srv.pageData.next(InitialPage);
      this.table?.moveToFistPage();
    }

    // Page data
    model.page = this.pageData;

    // Sort data
    model.sort = this.sortData;

    this.showSpinner = true;
    
    // Call server
    this.subSearch = this.srv.search(model).pipe(
      finalize(() => {
        this.showSpinner = false;
      })
    )
    .subscribe({
      next: (res: any) => {
        // Set data
        if (res.data) {
          // Set
          this.srv.srcRes.next(res.data);
          this.srv.showRes.next(true);
        } else {
          this.oprRes = createDORfromObj<ResModel<TransactionModel>>(res).getAlerts();
        }
      },
      error: (err: any) => {
        this.oprRes = createDORfromError<ResModel<TransactionModel>>(err).getAlerts();
      }
    });
  }

  onCsv(event: any) {
    const model = event as SearchTransactionModel;

    this.showSpinner = true;
    
    // Call server
    this.subCsv = this.appSrv.getTransactionCsv(model).pipe(
      finalize(() => {
        this.showSpinner = false;
      })
    )
    .subscribe({
      next: (res: any) => {
        // Set data
        if (res.data) {
          downloadFile(res.data);
        }

        this.oprRes = createDORfromObj<DownloadFileModel>(res).getAlerts();
      },
      error: (err: any) => {
        this.oprRes = createDORfromError<DownloadFileModel>(err).getAlerts();
      }
    });
  }

  onImport(event: any) {
    const model = event as SearchTransactionModel;

    this.showSpinner = true;
    
    // Call server
    this.subImport = this.srv.import(model).pipe(
      finalize(() => {
        this.showSpinner = false;
      })
    )
    .subscribe({
      next: (res: any) => {
        // Set data
        if (res.data) {
          // Set
          this.srv.srcRes.next(res.data);
          this.srv.showRes.next(true);
        } 

        this.oprRes = createDORfromObj<ResModel<TransactionModel>>(res).getAlerts();
      },
      error: (err: any) => {
        this.oprRes = createDORfromError<ResModel<TransactionModel>>(err).getAlerts();
      }
    });
  }

  onReset() {
    this.oprRes = null;
  }
}

