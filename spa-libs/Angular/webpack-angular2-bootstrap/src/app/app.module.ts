import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { HttpModule } from "@angular/http";
import { LocationStrategy, HashLocationStrategy } from "@angular/common";

import { AppComponent } from "./app.component";
import { BodyComponent } from "./../body/body.component";

import {
	TOASTR_TOKEN,
	JQ_TOKEN,
	POPPERJS_TOKEN
} from "./../wrappers/index";

declare let toastr: object;
declare let jQuery: object;
// declare let popper: object;

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
	{ provide: JQ_TOKEN, useValue: jQuery },
	{ provide: TOASTR_TOKEN, useValue: toastr },
	// { provide: POPPERJS_TOKEN, useValue: popper },
  ],
  bootstrap: [ AppComponent ]
})
export class AppModule {

}
