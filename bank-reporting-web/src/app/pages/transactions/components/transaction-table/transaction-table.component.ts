import { AfterViewInit, Component, EventEmitter, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { TransactionService } from '../../../../services/transaction.service';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { TransactionModel } from '../../models/transaction-model';
import { SearchTransactionModel } from '../../models/search-transaction-model';
import { PageModel } from '../../../../models/page-model';

@Component({
  selector: 'app-transaction-table',
  standalone: false,
  templateUrl: './transaction-table.component.html',
  styleUrl: './transaction-table.component.scss'
})
export class TransactionTableComponent implements OnInit, OnDestroy, AfterViewInit {

  // Table structure
  displayedColumns: string[] = ['partnerName', 'merchantName', 'debtorIban', 'beneficiaryIban', 'createDate', 'amount', 'externalId', 'status'];

  // Locals
  dataSource: MatTableDataSource<TransactionModel>;
  total = 0;
  srcFormModel: SearchTransactionModel | null = null;
  pageData!: PageModel;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator | null = null;
  @ViewChild(MatSort, { static: true }) sort: MatSort | null = null;

  // Subscription
  subRes: Subscription | null = null;
  subSrcForm: Subscription | null = null;
  subPageData: Subscription | null = null;
  subSortChange: Subscription | null = null;

  // Outputs
  @Output() public search = new EventEmitter<SearchTransactionModel>();

  constructor(private srv: TransactionService) {
    this.dataSource = new MatTableDataSource<TransactionModel>();
  }

  ngOnInit(): void {
    // Subscribe for data
    this.subRes = this.srv.srcRes.subscribe(res => {
      // Set data
      if (res !== null && res.res !== null) {
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
    this.subSortChange?.unsubscribe();
  }

  ngAfterViewInit() {
    this.subSortChange = this.sort!.sortChange
    .subscribe((sort: any) => {
        this.srv.sortData.next({ sortBy: sort.active, sortDirection: sort.direction });

        // Search
        var model = this.srcFormModel ? this.srcFormModel : <SearchTransactionModel>{};
        this.search.emit(model);
    });
  }

  // Show or hide table
  public get hasResults(): boolean {
    return this.dataSource.data && this.dataSource.data.length > 0;
  }

  onPageChange(event: any) {
    const evt = event as PageEvent;
    const page = <PageModel> { pageIndex: evt.pageIndex, pageSize: evt.pageSize };
    this.srv.pageData.next(page);

    // Search
    var model = this.srcFormModel ? this.srcFormModel : <SearchTransactionModel>{};
    model.isPaging = true;
    this.search.emit(model);
  }

  moveToFistPage() {
    this.paginator?.firstPage();
  }
}
