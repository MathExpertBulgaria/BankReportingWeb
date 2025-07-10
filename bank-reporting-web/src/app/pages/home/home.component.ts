import { Component, OnDestroy } from '@angular/core';
import { AppService } from '../../services/app.service';
import { SearchPartnerModel } from '../partners/models/search-partner-model';
import { finalize, Subscription } from 'rxjs';
import { b64toBlob } from '../../functions/b64toBlob';
import { createDORfromError, createDORfromObj, OperationAlert } from '../../models/data-operation-result-model';
import { DownloadFileModel } from '../../models/download-file.model';
import { SearchMerchantModel } from '../merchants/models/search-merchant-model';
import { SearchTransactionModel } from '../transactions/models/search-transaction-model';
import { downloadFile } from '../../functions/download-file';

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
  subMerchantCsv: Subscription | null = null;
  subTransactionCsv: Subscription | null = null;

  constructor(private appSrv: AppService) {

  }

  ngOnDestroy(): void {
    // Free
    this.subPartnerCsv?.unsubscribe();
    this.subMerchantCsv?.unsubscribe();
    this.subTransactionCsv?.unsubscribe();
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
          downloadFile(res.data);
        } 

        this.oprRes = createDORfromObj<DownloadFileModel>(res).getAlerts();
      },
      error: (err: any) => {
        this.oprRes = createDORfromError<DownloadFileModel>(err).getAlerts();
      }
    });
  }

  public onGetMerchantCsv() {
    this.showSpinner = true;

    // Call server
    this.subMerchantCsv = this.appSrv.getMerchantCsv(<SearchMerchantModel>{}).pipe(
      finalize(() => {
        this.showSpinner = false;
      })
    )
    .subscribe({
      next: (res: any) => {
        // Set data
        if (res.data) {
          downloadFile(res.data);
        } 

        this.oprRes = createDORfromObj<DownloadFileModel>(res).getAlerts();
      },
      error: (err: any) => {
        this.oprRes = createDORfromError<DownloadFileModel>(err).getAlerts();
      }
    });
  }

  public onGetTransactionCsv() {
    this.showSpinner = true;

    // Call server
    this.subTransactionCsv = this.appSrv.getTransactionCsv(<SearchTransactionModel>{}).pipe(
      finalize(() => {
        this.showSpinner = false;
      })
    )
    .subscribe({
      next: (res: any) => {
        // Set data
        if (res.data) {
          downloadFile(res.data);
        } 

        this.oprRes = createDORfromObj<DownloadFileModel>(res).getAlerts();
      },
      error: (err: any) => {
        this.oprRes = createDORfromError<DownloadFileModel>(err).getAlerts();
      }
    });
  }
}
