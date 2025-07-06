import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subscription, Observable } from 'rxjs';
import { DataOperationResult } from '../models/core/data-operation-result-model';
import { DownloadFileModel } from '../models/core/download-file.model';
import { SearchPartnerModel } from '../models/partner/search-partner-model';
import { SearchMerchantModel } from '../models/merchant/search-merchant-model';

@Injectable({
  providedIn: 'root'
})
export class AppService {

  // Url
  public static getPartnerCsvUrl = 'api/partner/GetCsv';
  public static getMerchantCsvUrl = 'api/merchant/GetCsv';

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
}
