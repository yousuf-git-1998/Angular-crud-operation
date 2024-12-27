import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { NavBarComponent } from './components/common/nav-bar/nav-bar.component';

import { HomeComponent } from './components/common/home/home.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { DeviceService } from './services/device.service';
import { NotifyService } from './services/notify.service';
import { MatImportModule } from './modules/mat-import/mat-import.module';
import { MatNativeDateModule } from '@angular/material/core';
import { ReactiveFormsModule } from '@angular/forms';
import { DeviceListComponent } from './components/device/device-list/device-list.component';
import { DeviceCreateComponent } from './components/device/device-create/device-create.component';
import { DeviceEditComponent } from './components/device/device-edit/device-edit.component';
import { SpecDialogComponent } from './components/common/spec-dialog/spec-dialog.component';
import { ConfirmDeleteComponent } from './components/common/confirm-delete/confirm-delete.component';
import { DatePipe } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    HomeComponent,
    DeviceListComponent,
    DeviceCreateComponent,
    DeviceEditComponent,
    SpecDialogComponent,
    ConfirmDeleteComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    MatImportModule,
   MatNativeDateModule,
   ReactiveFormsModule
   
  ],
  providers: [
    provideAnimationsAsync(),DatePipe, HttpClient, DeviceService, NotifyService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
