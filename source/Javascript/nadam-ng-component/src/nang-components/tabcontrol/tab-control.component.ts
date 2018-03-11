import { Component, OnInit, Input } from "@angular/core";
import { Tab } from "./tab.component";

@Component({
	selector: "tab-control",
	templateUrl: "./tab-control.component.html",
})
export class TabControl {
	tabs: Tab[] = [];

	addTab(tab:Tab): void {
		if (this.tabs.length === 0) {
			tab.active = true;
		}
		this.tabs.push(tab);
	}

	selectTab(tab: Tab): void {
		this.tabs.forEach((tab) => {
			tab.active = false;
		});
		tab.active = true;
	}
}