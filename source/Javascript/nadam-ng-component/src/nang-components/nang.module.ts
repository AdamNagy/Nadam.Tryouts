import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import {
	CollapsibleComponent,
	CollapsibleItemComponent
} from "./collapsible/index";

import {
	SlideComponent,
	SliderComponent
} from "./slider/index";

import {
	Tab,
	TabControl
} from "./tabcontrol/index";

import { FocusLayerComponent } from "./focus-layer/focuslayer.component";

import {
	GridComponent,
	GridCellComponent
} from "./grid/index";

@NgModule({
	imports: [
		BrowserModule
	],
	declarations: [
		CollapsibleComponent,
		CollapsibleItemComponent,
		FocusLayerComponent,
		GridComponent,
		GridCellComponent,
		SlideComponent,
		SliderComponent,
		Tab,
		TabControl
	],
	exports: [
		CollapsibleComponent,
		CollapsibleItemComponent,
		FocusLayerComponent,
		GridComponent,
		GridCellComponent,
		SlideComponent,
		SliderComponent,
		Tab,
		TabControl
	]
})
export class NaNgModules {}