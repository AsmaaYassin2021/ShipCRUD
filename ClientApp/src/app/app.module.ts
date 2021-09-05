import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {  HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { FloatNumbersDirective } from './_directives/float-numbers.directive';
import { ShipService } from './_services/ship.service';
import { HomeComponent } from './home';

@NgModule({
  declarations: [
    AppComponent,
    FloatNumbersDirective,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule ,
    FormsModule,
    ReactiveFormsModule,
    NgxPaginationModule,
    AppRoutingModule
    
  ],
  providers: [ShipService],
  bootstrap: [AppComponent]
})
export class AppModule { }
