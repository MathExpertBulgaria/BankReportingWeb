import { Component, OnDestroy } from '@angular/core';
import { AppService } from '../../services/app.service';
import { SearchPartnerModel } from '../../models/partner/search-partner-model';
import { finalize, Subscription } from 'rxjs';
import { b64toBlob } from '../../functions/b64toBlob';
import { createDORfromError, createDORfromObj, OperationAlert } from '../../models/core/data-operation-result-model';
import { DownloadFileModel } from '../../models/core/download-file.model';
import { SearchMerchantModel } from '../../models/merchant/search-merchant-model';

@Component({
  selector: 'app-home',
  standalone: false,
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnDestroy {

  public oprRes: OperationAlert[] | null = null;
  public showSpinner = false;

    // Subscription
  subPartnerCsv: Subscription | null = null;

  constructor(private appSrv: AppService) {

  }

  ngOnDestroy(): void {
    // Free
    this.subPartnerCsv?.unsubscribe();
  }

  public onGetPartnerCsv() {
    this.showSpinner = true;

    // Call server
    this.subPartnerCsv = this.appSrv.getPartnerCsv(<SearchPartnerModel>{}).pipe(
      finalize(() => {
        this.showSpinner = false;
      })
    )
    .subscribe({
      next: (res: any) => {
        // Set data
        if (res.data) {
          const blob = b64toBlob(res.data.contents, res.data.contentType);
          const a = document.createElement('a');
          const url = window.URL.createObjectURL(blob);
      
          document.body.appendChild(a);
          a.href = url;
          a.download = res.data.filename;
          a.click();
        } else {
          this.oprRes = createDORfromObj<DownloadFileModel>(res).getAlerts();
        }
      },
      error: (err: any) => {
        this.oprRes = createDORfromError<DownloadFileModel>(err).getAlerts();
      }
    });
  }

  public onGetMerchantCsv() {
    this.showSpinner = true;

    // Call server
    this.subPartnerCsv = this.appSrv.getMerchantCsv(<SearchMerchantModel>{}).pipe(
      finalize(() => {
        this.showSpinner = false;
      })
    )
    .subscribe({
      next: (res: any) => {
        // Set data
        if (res.data) {
          const blob = b64toBlob(res.data.contents, res.data.contentType);
          const a = document.createElement('a');
          const url = window.URL.createObjectURL(blob);
      
          document.body.appendChild(a);
          a.href = url;
          a.download = res.data.filename;
          a.click();
        } else {
          this.oprRes = createDORfromObj<DownloadFileModel>(res).getAlerts();
        }
      },
      error: (err: any) => {
        this.oprRes = createDORfromError<DownloadFileModel>(err).getAlerts();
      }
    });
  }
}
