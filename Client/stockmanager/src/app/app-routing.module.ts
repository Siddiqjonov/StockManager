import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BalanceComponent } from './components/balance/balance.component';
import { ReceiptComponent } from './components/receipt/receipt.component';
import { ShipmentComponent } from './components/shipment/shipment.component';
import { ClientComponent } from './components/client/client.component';
import { MeasurementUnitComponent } from './components/measurement-unit/measurement-unit.component';
import { ResourcesComponent } from './components/resources/resources.component';
import { MainLayoutComponent } from './components/main-layout/main-layout.component';

// const routes: Routes = [
//   {path: '', component: BalanceComponent, pathMatch: 'full'},
//   {path: 'recipts', component: ReceiptComponent},
//   {path: 'shipments', component: ShipmentComponent},
//   {path: 'clients', component: ClientComponent},
//   {path: 'measurement-unit', component: MeasurementUnitComponent},
//   {path: 'resources', component: ResourcesComponent},
//   {path: '**', redirectTo: ''},
// ];

const routes: Routes = [
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      { path: '', component: BalanceComponent, pathMatch: 'full' },
      { path: 'recipts', component: ReceiptComponent },
      { path: 'shipments', component: ShipmentComponent },
      { path: 'clients', component: ClientComponent },
      { path: 'measurement-unit', component: MeasurementUnitComponent },
      { path: 'resources', component: ResourcesComponent },
      { path: '**', redirectTo: '' }
    ]
  }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
