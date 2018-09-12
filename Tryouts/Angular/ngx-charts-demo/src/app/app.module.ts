import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { AppComponent } from "./app.component";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { HttpModule } from "@angular/http";

import { LocationStrategy, HashLocationStrategy } from "@angular/common";
// import * as $ from "jquery";
// import "bootstrap";
// import "bootstrap/dist/css/bootstrap.min.css";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import { NgxChartsModule } from "@swimlane/ngx-charts";

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
      BrowserModule,
      FormsModule,
      ReactiveFormsModule,
      HttpModule,
      NgxChartsModule,
      BrowserAnimationsModule
  ],
  providers: [  ],
  bootstrap: [ AppComponent ]
})
export class AppModule {

}
