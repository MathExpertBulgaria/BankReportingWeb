import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SearchTransactionModel } from '../models/transaction/search-transaction-model';
import { ResModel } from '../models/core/res-model';
import { TransactionModel } from '../models/transaction/transaction-model';
import { BehaviorSubject, Observable } from 'rxjs';
import { DataOperationResult } from '../models/core/data-operation-result-model';
import { DownloadFileModel } from '../models/core/download-file.model';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  // Url
  public static searchUrl = 'api/transaction/Search';
  public static getCsvUrl = 'api/transaction/GetCsv';

  // Seach model
  public srcModel: BehaviorSubject<SearchTransactionModel | null> = new BehaviorSubject<SearchTransactionModel | null>(null);
  // Search result
  public srcRes: BehaviorSubject<ResModel<TransactionModel> | null> = new BehaviorSubject<ResModel<TransactionModel> | null>(null);

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
  public gettCsv(model: SearchTransactionModel): Observable<DataOperationResult<DownloadFileModel>> {
    // Call server
    return this.http.post<DataOperationResult<DownloadFileModel>>(TransactionService.getCsvUrl, model);
  }
}
