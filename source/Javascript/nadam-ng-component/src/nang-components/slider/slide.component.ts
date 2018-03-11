import { Component, Input } from "@angular/core";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";

import { SliderComponent } from "./slider.component";
import {
	trigger,
	state,
	style,
	animate,
	transition
  } from "@angular/animations";

@Component({
	selector: "slide",
	templateUrl: "./slide.component.html",
	animations: [
		trigger("slideState", [
		  state("true", style({
			opacity: "1",
			display: "block",
			left: "0"
		  })),
		  state("false",   style({
			opacity: "0",
			display: "none",
			left: "800px"
		  })),
		  transition("false => true", animate("400ms ease-in"))
		])
	]
})
export class SlideComponent {
	@Input() title: string;
	active: boolean;
	id: number;

	constructor(slides: SliderComponent) {
		slides.addSlide(this);
	}
}
