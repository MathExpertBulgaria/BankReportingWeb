import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { MenuComponent } from './pages/menu/menu.component';
import { InfoComponent } from './pages/info/info.component';
import { PageNotFoundComponent } from './pages/page-not-found/page-not-found.component';
import { TransactionsComponent } from './pages/transactions/transactions.component';

const routes: Routes = [{
  path: '', component: MenuComponent,
  children: [
    { path: '', component: HomeComponent, data: { title: 'Home' } },
    { path: 'transactions', component: TransactionsComponent, data: { title: 'Transactions' } },

    { path: 'info', component: InfoComponent, data: { title: 'Information' } },

    { path: '**', component: PageNotFoundComponent, data: { title: 'Page not found' } }
  ]
}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
