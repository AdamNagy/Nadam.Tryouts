import { Component } from "@angular/core";

@Component({
  templateUrl: "./focus-layer-demo.component.html"
})
export class FocusLayerDemoComponent {

	showFocusLayer: boolean = false;

	someLongTakingMethod(): void {
		this.showFocusLayer = true;
	}
}
