import { Injectable } from '@angular/core';
import { PartnerModel } from '../pages/partners/models/partner-model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DataOperationResult } from '../models/data-operation-result-model';
import { ResModel } from '../models/res-model';
import { SearchPartnerModel } from '../pages/partners/models/search-partner-model';

@Injectable({
  providedIn: 'root'
})
export class PartnerService {

  // Url
  public static searchUrl = 'api/partner/Search';

  constructor(private http: HttpClient) {

  }

  // Search
  public search(model: SearchPartnerModel): Observable<DataOperationResult<ResModel<PartnerModel>>> {
    // Call server
    return this.http.post<DataOperationResult<ResModel<PartnerModel>>>(PartnerService.searchUrl, model);
  }
}
