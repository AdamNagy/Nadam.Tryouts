import { Component, Input, EventEmitter, Output, OnInit } from "@angular/core";
import { GridCellComponent } from "./grid-cell.component";
import { forEach } from "@angular/router/src/utils/collection";
// declare var jquery:any; declare var $ :any;

@Component({
	selector: "grid",
	templateUrl: "./grid.component.html",
	styleUrls: ["./grid.component.css"]
})
export class GridComponent implements OnInit {

	@Input()
	columns: number;
	rows: number;
	children: GridCellComponent[] = [];
	cells: Array<Array<GridCellComponent>> = new Array<Array<GridCellComponent>>();

	columnCounter: number = 0;
	rowCounter: number = 0;

	ngOnInit(): void {
		this.rows = this.cells.length / this.columns;
		// let colwith: string = this.getColumsWidth().toString();
		// $("#mygrid div.cell").css("width", "20%");
		// $("div.cell").css("color", "red");
		// $("div:first-child").css("width", "400px");
		// $("div:first-child").css("color", "red");
		// $("div:first-child").css("width", "300px !important");
		// $("div.grid-cell").css("color", "red");
		// $("div.cell").addClass("quarterWidth");
		// $("div.cell").css("color", "yellow");
		// console.log(this.columns);
		// this.children.forEach(this.layoutCell);
		// $("div.cell").css("color", "blue");
		for(let idx: number = 0; idx < this.children.length; ++idx) {
			this.layoutCell(this.children[idx]);
		}
	}

	addChild(child: GridCellComponent): void {
		this.children.push(child);
	}

	layoutCell(newCell: GridCellComponent): void {

		if( this.columnCounter === this.rowCounter ) {
			this.cells.push(new Array<GridCellComponent>());
		}

		if( this.columnCounter >= this.columns ) {
			this.columnCounter = 0;
			++this.rowCounter;
			this.cells[this.rowCounter] = [];
		}

		this.cells[this.rowCounter].push(newCell);
		++this.columnCounter;
	}

	// getColumsWidth(): number {
	// 	switch(this.columns) {
	// 		case 1: return 100;
	// 		case 2: return 50;
	// 	}
	// }
}