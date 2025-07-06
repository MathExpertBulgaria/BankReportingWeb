import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule, MatIconRegistry } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatFormFieldModule } from '@angular/material/form-field';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MenuComponent } from './pages/menu/menu.component';
import { HomeComponent } from './pages/home/home.component';
import { InfoComponent } from './pages/info/info.component';
import { provideHttpClient } from '@angular/common/http';
import { PageNotFoundComponent } from './pages/page-not-found/page-not-found.component';
import { OperationResultComponent } from './components/operation-result/operation-result.component';
import { AlertComponent } from './components/alert/alert.component';
import { ProgressUndeterminateComponent } from './components/progress-undeterminate/progress-undeterminate.component';
import { TransactionsComponent } from './pages/transactions/transactions.component';
import { SearchTransactionComponent } from './pages/transactions/components/search-transaction/search-transaction.component';
import { TransactionTableComponent } from './pages/transactions/components/transaction-table/transaction-table.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule, MatDatepickerToggle } from '@angular/material/datepicker';

@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    HomeComponent,
    InfoComponent,
    PageNotFoundComponent,
    OperationResultComponent,
    AlertComponent,
    ProgressUndeterminateComponent,
    TransactionsComponent,
    SearchTransactionComponent,
    TransactionTableComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    MatToolbarModule,
    MatIconModule,
    MatMenuModule,
    MatTooltipModule,
    MatButtonModule,
    MatCardModule,
    MatDividerModule,
    MatProgressSpinnerModule,
    MatFormFieldModule,
    MatExpansionModule,
    MatSelectModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatDatepickerToggle
  ],
  providers: [
    MatIconRegistry,
    provideHttpClient()
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(public matIconRegistry: MatIconRegistry) {
     matIconRegistry.registerFontClassAlias('fontawesome', 'fa');
  }
 }
