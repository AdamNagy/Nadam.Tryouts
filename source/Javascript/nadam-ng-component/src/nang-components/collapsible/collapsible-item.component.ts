import { Component, OnInit, Input } from "@angular/core";
import { CollapsibleComponent } from "./collapsible.component";

@Component({
	selector: "collapsible-item",
	templateUrl: "./collapsible-item.component.html",
})
export class CollapsibleItemComponent {

	id: number;
	group: String;
	@Input() title: String;
	@Input() text: String;

	constructor(parent: CollapsibleComponent) {
		parent.addCollapsable(this);
	}
}
