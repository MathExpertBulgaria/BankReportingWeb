import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subscription, Observable } from 'rxjs';
import { DataOperationResult } from '../models/data-operation-result-model';
import { DownloadFileModel } from '../models/download-file.model';
import { SearchPartnerModel } from '../pages/partners/models/search-partner-model';
import { SearchMerchantModel } from '../pages/merchants/models/search-merchant-model';
import { SearchTransactionModel } from '../pages/transactions/models/search-transaction-model';

@Injectable({
  providedIn: 'root'
})
export class AppService {

  // Url
  public static getPartnerCsvUrl = 'api/partner/GetCsv';
  public static getMerchantCsvUrl = 'api/merchant/GetCsv';
  public static getTransactionCsvUrl = 'api/transaction/GetCsv';

  constructor(private http: HttpClient) {

   }

  // Get partner csv
  public getPartnerCsv(model: SearchPartnerModel): Observable<DataOperationResult<DownloadFileModel>> {
    // Call server
    return this.http.post<DataOperationResult<DownloadFileModel>>(AppService.getPartnerCsvUrl, model);
  }

  // Get merchant csv
  public getMerchantCsv(model: SearchMerchantModel): Observable<DataOperationResult<DownloadFileModel>> {
    // Call server
    return this.http.post<DataOperationResult<DownloadFileModel>>(AppService.getMerchantCsvUrl, model);
  }

  // Get transaction csv
  public getTransactionCsv(model: SearchTransactionModel): Observable<DataOperationResult<DownloadFileModel>> {
    // Call server
    return this.http.post<DataOperationResult<DownloadFileModel>>(AppService.getTransactionCsvUrl, model);
  }
}
