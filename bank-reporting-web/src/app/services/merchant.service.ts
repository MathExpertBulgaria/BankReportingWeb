import { Injectable } from '@angular/core';
import { SearchMerchantModel } from '../pages/merchants/models/search-merchant-model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DataOperationResult } from '../models/data-operation-result-model';
import { ResModel } from '../models/res-model';

@Injectable({
  providedIn: 'root'
})
export class MerchantService {

  // Url
  public static searchUrl = 'api/merchant/Search';

  constructor(private http: HttpClient) {

  }

  // Search
  public search(model: SearchMerchantModel): Observable<DataOperationResult<ResModel<SearchMerchantModel>>> {
    // Call server
    return this.http.post<DataOperationResult<ResModel<SearchMerchantModel>>>(MerchantService.searchUrl, model);
  }
}
