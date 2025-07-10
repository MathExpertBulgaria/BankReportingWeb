import { Component, EventEmitter, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { TransactionService } from '../../../../services/transaction.service';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { TransactionModel } from '../../models/transaction-model';
import { SearchTransactionModel } from '../../models/search-transaction-model';

@Component({
  selector: 'app-transaction-table',
  standalone: false,
  templateUrl: './transaction-table.component.html',
  styleUrl: './transaction-table.component.scss'
})
export class TransactionTableComponent implements OnInit, OnDestroy {

  // Table structure
  displayedColumns: string[] = ['partnerName', 'merchantName', 'debtorIban', 'beneficiaryIban', 'createDate', 'amount', 'externald', 'status'];

  // Locals
  dataSource: MatTableDataSource<TransactionModel>;
  total = 0;
  srcFormModel: SearchTransactionModel | null = null;
  pageData!: PageEvent;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator | null = null;
  @ViewChild(MatSort, { static: true }) sort: MatSort | null = null;

  // Subscription
  subRes: Subscription | null = null;
  subSrcForm: Subscription | null = null;
  subPageData: Subscription | null = null;

  // Outputs
  @Output() public search = new EventEmitter<SearchTransactionModel>();

  constructor(private srv: TransactionService) {
    this.dataSource = new MatTableDataSource<TransactionModel>();
  }

  ngOnInit(): void {
    // Define paging and sort
    // this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    if (this.sort) {
     this.sort.active = 'id';
     this.sort.start = 'asc';
    }

    // Subscribe for data
    this.subRes = this.srv.srcRes.subscribe(res => {
      // Set data
      if (res !== null && res.res !== null) {
        // Re-set sort
        if (this.sort) {
         this.sort.active = 'id';
         this.sort.direction = 'asc';
        }

        this.dataSource.data = res.res!;
        this.total = res.total;

        this.dataSource._updateChangeSubscription();
      } else {
        this.dataSource.data = [];
        this.dataSource._updateChangeSubscription();
      }
    });

    // Search
    this.subSrcForm = this.srv.srcFormModel.subscribe(x => {
      this.srcFormModel = x;
    });

    // Page
    this.subPageData = this.srv.pageData.subscribe(x => {
      this.pageData = x;
    })
  }

  ngOnDestroy() {
    // Free
    this.subRes?.unsubscribe();
    this.subSrcForm?.unsubscribe();
    this.subPageData?.unsubscribe();
  }

  // Show or hide table
  public get hasResults(): boolean {
    return this.dataSource.data && this.dataSource.data.length > 0;
  }

  onPageChange(event: any) {
    const evt = event as PageEvent;
    this.srv.pageData.next(evt);

    var model = this.srcFormModel ? this.srcFormModel : <SearchTransactionModel>{};

    this.search.emit(model);
  }
}
