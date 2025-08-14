import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration, withEventReplay } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BalanceComponent } from './components/balance/balance.component';
import { ReceiptComponent } from './components/receipt/receipt.component';
import { ShipmentComponent } from './components/shipment/shipment.component';
import { ClientComponent } from './components/client/client.component';
import { MeasurementUnitComponent } from './components/measurement-unit/measurement-unit.component';
import { ResourcesComponent } from './components/resources/resources.component';
import { MainLayoutComponent } from './components/main-layout/main-layout.component';

@NgModule({
  declarations: [
    AppComponent,
    BalanceComponent,
    ReceiptComponent,
    ShipmentComponent,
    ClientComponent,
    MeasurementUnitComponent,
    ResourcesComponent,
    MainLayoutComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [
    provideClientHydration(withEventReplay())
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
