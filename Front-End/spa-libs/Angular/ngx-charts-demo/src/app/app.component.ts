import {Component, NgModule, OnInit} from "@angular/core";
import {BrowserModule} from "@angular/platform-browser";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {NgxChartsModule} from "@swimlane/ngx-charts";
import {single, multi} from "../data";
import * as $ from "jquery";

@Component({
  selector: "app",
  templateUrl: "./app.component.html",
})
export class AppComponent implements OnInit {

  showXAxis = true;
  showYAxis = true;
  gradient = false;
  showLegend = true;
  showXAxisLabel = true;
  xAxisLabel = "Country";
  showYAxisLabel = true;
  yAxisLabel = "Population";

  colorScheme = {
    domain: ["#5AA454", "#A10A28", "#C7B42C", "#AAAAAA"]
  };

  constructor() {
    Object.assign(this, {single, multi});
  }

  onSelect(event: any): void {
    console.log(event);
  }

  ngOnInit(): void {
    $("#qwe").css("color", "red");
  }
}
