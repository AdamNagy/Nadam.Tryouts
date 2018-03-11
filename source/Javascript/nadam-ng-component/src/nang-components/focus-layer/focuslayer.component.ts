import { Component, Input, EventEmitter, Output } from "@angular/core";

@Component({
	selector: "focuslayer",
	template: "<div (click)='hide()' [hidden]='!isShownValue'>  </div>",
	styleUrls: ["./focuslayer.component.css"]
})
export class FocusLayerComponent {

	@Input()
	canCloseByClicking: String = "false";

	isShownValue: boolean = false;
	@Input()
	get isShown(): boolean {
		return this.isShownValue;
	}


	@Output() isShownChange = new EventEmitter();
	set isShown(val: boolean) {
		this.isShownValue = val;
		this.isShownChange.emit(this.isShown);
	}

	hide(): void {
		if( this.canCloseByClicking === "true" ) {
			this.isShown = false;
		}
	}
}
