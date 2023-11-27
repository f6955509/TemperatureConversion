import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TemperatureService } from './services/temperature.service';
import { HttpRequestHandler } from './services/http.request.handler';
import { HttpClientModule, HttpClient } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule 
  ],
  providers: [TemperatureService, HttpRequestHandler,HttpClient],
  bootstrap: [AppComponent]
})
export class AppModule { }
