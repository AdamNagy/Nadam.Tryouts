import { Component, OnInit, Input } from "@angular/core";
import { TabControl } from "./tab-control.component";
import {
	trigger,
	state,
	style,
	animate,
	transition
  } from "@angular/animations";

@Component({
	selector: "tab",
	templateUrl: "./tab.component.html",
	styleUrls: ["./style.css"],
	animations: [
		trigger("tabState", [
		  state("true", style({
			opacity: "1",
			display: "block"
		  })),
		  state("false",   style({
			opacity: "0",
			display: "none"
		  })),
		  transition("false => true", animate("400ms ease-in"))
		])
	]
})
export class Tab {
	@Input() tabTitle;
	active: boolean = false;

	constructor(tabs: TabControl) {
		tabs.addTab(this);
	}
}