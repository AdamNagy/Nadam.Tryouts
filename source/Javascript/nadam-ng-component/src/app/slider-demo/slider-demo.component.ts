import { Component, OnInit, Inject} from "@angular/core";

@Component({
	templateUrl: "./slider-demo.component.html",
})
export class SliderDemoComponent implements OnInit {
	slides: number[] = [];

	ngOnInit(): void {
		for(let i:number = 1; i < 5; i++) {
			this.slides.push(i);
		}
	}
}
