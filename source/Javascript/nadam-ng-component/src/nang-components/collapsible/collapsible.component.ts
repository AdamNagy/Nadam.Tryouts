import { Component, Input } from "@angular/core";
import { CollapsibleItemComponent } from "./collapsible-item.component";

@Component({
	selector: "collapsible",
	template: "<ng-content></ng-content>",
})
export class CollapsibleComponent {

	@Input() groupName: String;
	collapsables: CollapsibleItemComponent[] = [];
	numOfCollapsables: number = 0;

	addCollapsable(item: CollapsibleItemComponent): void {
		item.id = ++this.numOfCollapsables;
		item.group = this.groupName;
		this.collapsables.push(item);
		let s: string = "sdf";
	}
}
