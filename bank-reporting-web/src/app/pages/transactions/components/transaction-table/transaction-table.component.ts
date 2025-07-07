import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { TransactionService } from '../../../../services/transaction.service';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { TransactionModel } from '../../models/transaction-model';

@Component({
  selector: 'app-transaction-table',
  standalone: false,
  templateUrl: './transaction-table.component.html',
  styleUrl: './transaction-table.component.scss'
})
export class TransactionTableComponent implements OnInit, OnDestroy {

  // Table structure
  displayedColumns: string[] = ['id'];

  dataSource: MatTableDataSource<TransactionModel>;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator | null = null;
  @ViewChild(MatSort, { static: true }) sort: MatSort | null = null;

  // Subscription
  subRes: Subscription | null = null;

  usersPageSize?: number;

  constructor(private srv: TransactionService) {
    this.dataSource = new MatTableDataSource<TransactionModel>();
  }

  ngOnInit(): void {
    // Define paging and sort
    this.dataSource.paginator = this.paginator;
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

        this.dataSource._updateChangeSubscription();
      } else {
        this.dataSource.data = [];
        this.dataSource._updateChangeSubscription();
      }
    });
  }

  ngOnDestroy() {
    // Free
    this.subRes?.unsubscribe();
  }

  // Show or hide table
  public get hasResults(): boolean {
    return this.dataSource.data && this.dataSource.data.length > 0;
  }

  onPageSizeChange(event: any) {
    const evt = event as PageEvent;
  }
}
