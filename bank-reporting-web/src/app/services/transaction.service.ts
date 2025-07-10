import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SearchTransactionModel } from '../pages/transactions/models/search-transaction-model';
import { ResModel } from '../models/res-model';
import { TransactionModel } from '../pages/transactions/models/transaction-model';
import { BehaviorSubject, Observable } from 'rxjs';
import { DataOperationResult } from '../models/data-operation-result-model';
import { DownloadFileModel } from '../models/download-file.model';
import { PageEvent } from '@angular/material/paginator';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  // Url
  public static searchUrl = 'api/transaction/Search';
  public static getCsvUrl = 'api/transaction/GetCsv';
  public static importUrl = 'api/transaction/ImportXml';

  // Seach model
  public srcModel: BehaviorSubject<SearchTransactionModel | null> = new BehaviorSubject<SearchTransactionModel | null>(null);
  public srcModelInit: SearchTransactionModel | null = null;
  // Search form
  public srcFormModel: BehaviorSubject<SearchTransactionModel | null> = new BehaviorSubject<SearchTransactionModel | null>(null);
  // Search result
  public srcRes: BehaviorSubject<ResModel<TransactionModel> | null> = new BehaviorSubject<ResModel<TransactionModel> | null>(null);
  // Show result
  public showRes: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  // Page data
  public pageData: BehaviorSubject<PageEvent> = new BehaviorSubject<PageEvent>(<PageEvent> {
    pageIndex: 0,
    pageSize: 5
  });

  constructor(private http: HttpClient) {

  }

  // Get
  public get(): Observable<DataOperationResult<SearchTransactionModel>> {
    // Call server
    return this.http.get<DataOperationResult<SearchTransactionModel>>(TransactionService.searchUrl);
  }

  // Search
  public search(model: SearchTransactionModel): Observable<DataOperationResult<ResModel<TransactionModel>>> {
    // Call server
    return this.http.post<DataOperationResult<ResModel<TransactionModel>>>(TransactionService.searchUrl, model);
  }

  // Get csv
  public getCsv(model: SearchTransactionModel): Observable<DataOperationResult<DownloadFileModel>> {
    // Call server
    return this.http.post<DataOperationResult<DownloadFileModel>>(TransactionService.getCsvUrl, model);
  }

  // Import xml
  public import(model: SearchTransactionModel): Observable<DataOperationResult<ResModel<TransactionModel>>> {
    // Call server
    return this.http.post<DataOperationResult<ResModel<TransactionModel>>>(TransactionService.importUrl, model);
  }
}
