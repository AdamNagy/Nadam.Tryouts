import { Component, OnInit, OnChanges } from "@angular/core";
import { SlideComponent } from "./slide.component";

@Component({
	selector: "slider",
	templateUrl: "./slider.component.html",
	styleUrls: ["./slider.component.css"]
})
export class SliderComponent implements OnInit, OnChanges {

	slides: SlideComponent[] = [];
	activeSlideId: number = 0;
	numOfSlides: number = 0;

	ngOnInit(): void {
		this.numOfSlides = this.slides.length;
		this.activeSlideId = 1;
	}

	ngOnChanges(): void {
		this.deactivateAllSlide();
		this.changeActiveSlide();
	}

	addSlide(slide: SlideComponent): void {
		slide.id = ++this.numOfSlides;
		this.slides.push(slide);
	}

	changeActiveSlide(): void {
		console.log("SliderComponent " + this.numOfSlides);
		if( this.numOfSlides > 0 ) {
			this.slides[this.activeSlideId - 1].active = true;
		}
	}

	selectSlide(slide: SlideComponent): void {
		this.deactivateAllSlide();
		this.activeSlideId = slide.id;
		this.changeActiveSlide();
	}

	deactivateAllSlide(): void {
		this.slides.forEach((tab) => {
			tab.active = false;
		});
	}

	slideToNextPage(): void {
		this.deactivateAllSlide();
		this.activeSlideId++;
		if (this.activeSlideId > this.numOfSlides) {
			this.activeSlideId = 1;
		}
		this.changeActiveSlide();
	}

	slideToPrevPage(): void {
		this.deactivateAllSlide();
		this.activeSlideId--;
		if (this.activeSlideId < 1) {
			this.activeSlideId = this.numOfSlides;
		}
		this.changeActiveSlide();
	}
}
