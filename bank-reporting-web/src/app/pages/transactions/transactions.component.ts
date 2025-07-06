import { Component, OnDestroy, OnInit } from '@angular/core';
import { createDORfromError, createDORfromObj, OperationAlert } from '../../models/core/data-operation-result-model';
import { SearchTransactionModel } from '../../models/transaction/search-transaction-model';
import { finalize, Subscription } from 'rxjs';
import { TransactionService } from '../../services/transaction.service';
import { ResModel } from '../../models/core/res-model';
import { TransactionModel } from '../../models/transaction/transaction-model';

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

  // Subscriptions
  subGet: Subscription | null = null;
  subSearch: Subscription | null = null;

  constructor(private srv: TransactionService) {

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
        } else {
          this.oprRes = createDORfromObj<SearchTransactionModel>(res).getAlerts();
        }
      },
      error: (err: any) => {
        this.oprRes = createDORfromError<SearchTransactionModel>(err).getAlerts();
      }
    });
  }

  ngOnDestroy(): void {
    // Free
    this.subGet?.unsubscribe();
    this.subSearch?.unsubscribe();
  }

  onSearch(event: any) {
    const model = event as SearchTransactionModel;

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
  }

  onImport(event: any) {
    const model = event as SearchTransactionModel;
  }
}
