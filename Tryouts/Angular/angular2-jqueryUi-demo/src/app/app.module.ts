import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { HttpModule } from "@angular/http";
import { LocationStrategy, HashLocationStrategy } from "@angular/common";

import { AppComponent } from "./app.component";
import { BodyComponent } from "./../body/body.component";

// import {
// 	JQ_TOKEN } from "./../wrappers/index";

// declare let jQuery: object;


@NgModule({
  declarations: [
	AppComponent,
	BodyComponent
  ],
  imports: [
      BrowserModule,
      FormsModule,
      ReactiveFormsModule,
	  HttpModule
  ],
  providers: [
	// { provide: JQ_TOKEN, useValue: jQuery }
  ],
  bootstrap: [ AppComponent ]
})
export class AppModule {

}
