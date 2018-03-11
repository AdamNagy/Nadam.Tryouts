import { Component, Input, EventEmitter, Output } from "@angular/core";
import { GridComponent } from "./grid.component";

@Component({
	selector: "grid-cell",
	templateUrl: "./grid-cell.component.html",
	styleUrls: ["./grid-cell.component.css"]
})
export class GridCellComponent {

	@Input() title: string;

	constructor(parent: GridComponent) {
		parent.addChild(this);
	}
}