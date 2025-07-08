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
import { ProgressUndeterminateComponent } from './components/progress-undeterminate/progress-undeterminate.component';
import { TransactionsComponent } from './pages/transactions/transactions.component';
import { SearchTransactionComponent } from './pages/transactions/components/search-transaction/search-transaction.component';
import { TransactionTableComponent } from './pages/transactions/components/transaction-table/transaction-table.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule, MatDatepickerToggle } from '@angular/material/datepicker';
import { MAT_DATE_FORMATS } from '@angular/material/core';
import { LuxonDateModule, MAT_LUXON_DATE_ADAPTER_OPTIONS } from '@angular/material-luxon-adapter';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatAutocompleteModule } from '@angular/material/autocomplete';

const MY_NATIVE_DATE_FORMATS = {
  parse: {
    dateInput: [ 'LL/dd/yyyy' ],
    timeInput: [ 'HH:mm' ]
  },
  display: {
    dateInput: 'LL/dd/yyyy',
    monthYearLabel: 'LLL y',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'LLLL y',
    timeInput: 'HH:mm',
    timeOptionLabel: 'HH:mm'
  }
};

@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    HomeComponent,
    InfoComponent,
    PageNotFoundComponent,
    OperationResultComponent,
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
    MatDatepickerToggle,
    MatDatepickerModule,
    LuxonDateModule,
    MatInputModule,
    MatPaginatorModule,
    MatSortModule,
    MatTableModule,
    MatAutocompleteModule,
    MatSortModule
  ],
  providers: [
    MatIconRegistry,
    provideHttpClient(),
    { provide: MAT_DATE_FORMATS, useValue: MY_NATIVE_DATE_FORMATS },
    { provide: MAT_LUXON_DATE_ADAPTER_OPTIONS, useValue: { useUtc: true, firstDayOfWeek: 7 } },
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(public matIconRegistry: MatIconRegistry) {
     matIconRegistry.registerFontClassAlias('fontawesome', 'fa');
  }
 }
