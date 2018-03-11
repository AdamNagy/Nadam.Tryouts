// angular
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { HttpModule } from "@angular/http";
import {
	LocationStrategy,
	HashLocationStrategy } from "@angular/common";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";

// na-ng-module
import { NaNgModules } from "./../nang-components/nang.module";
import { MathOperations } from "./../nang-components/nang.index";

// app
import { AppComponent } from "./app.component";
import { appRoutes } from "./routes";

// other from this project
import { CollapsableDemoComponent } from "./collapsable-demo/collapsable-demo.component";
import { SliderDemoComponent } from "./slider-demo/slider-demo.component";
import { TabControlDemoComponent } from "./tab-control-demo/tab-control-demo.component";
import { FooterComponent } from "./footer/footer.component";
import { FocusLayerDemoComponent } from "./focus-layer-demo/focus-layer-demo.component";
import { GridDemoComponent } from "./grid-demo/grid-demo.component";
import {
	TOASTR_TOKEN,
	JQ_TOKEN
} from "./wrappers/index";

// third party npm packages

// opaque tokens
declare let toastr: object;
declare let jQuery: object;

@NgModule({
  declarations: [
	AppComponent,
	CollapsableDemoComponent,
	FocusLayerDemoComponent,
	FooterComponent,
	GridDemoComponent,
	SliderDemoComponent,
	TabControlDemoComponent
  ],
  imports: [
	  BrowserModule,
	  FormsModule,
	  NaNgModules,
      ReactiveFormsModule,
	  HttpModule,
	  BrowserAnimationsModule,
	  RouterModule.forRoot(appRoutes)
  ],
  providers: [
	MathOperations,
	{ provide: JQ_TOKEN, useValue: jQuery },
	{ provide: TOASTR_TOKEN, useValue: toastr },
  ],
  bootstrap: [ AppComponent ]
})
export class AppModule {

}
