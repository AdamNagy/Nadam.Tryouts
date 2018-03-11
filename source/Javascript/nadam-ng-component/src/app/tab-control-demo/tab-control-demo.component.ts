import { Component, OnInit, Inject} from "@angular/core";

@Component({
	templateUrl: "./tab-control-demo.component.html",
})
export class TabControlDemoComponent implements OnInit {
	tabs: number[] = [];

	ngOnInit(): void {
		for(let i:number = 1; i < 5; i++) {
			this.tabs.push(i);
		}
	}
}
