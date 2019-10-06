import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { AppComponent } from "./app.component";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { HttpModule } from "@angular/http";

import { LocationStrategy, HashLocationStrategy } from "@angular/common";
import { ChartsModule } from "ng2-charts";
import { BarChartDemoComponent } from "./barchart.component";
// import * as $ from "jquery";
// import "bootstrap";
// import "bootstrap/dist/css/bootstrap.min.css";

@NgModule({
  declarations: [
	AppComponent,
	BarChartDemoComponent
  ],
  imports: [
      BrowserModule,
      FormsModule,
      ReactiveFormsModule,
      HttpModule,
	  ChartsModule
  ],
  providers: [  ],
  bootstrap: [ AppComponent ]
})
export class AppModule {

}
