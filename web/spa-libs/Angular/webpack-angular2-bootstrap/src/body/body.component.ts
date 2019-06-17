import { Component, OnInit, Inject} from "@angular/core";
import { JQ_TOKEN, TOASTR_TOKEN } from "./../wrappers/index";
import Popper from "popper.js";


@Component({
  selector: "app-body",
  templateUrl: "./body.component.html",
  styleUrls: ["./body.component.css"]
})
export class BodyComponent implements OnInit {

	constructor(
		@Inject(TOASTR_TOKEN) private toastr: any,
		@Inject(JQ_TOKEN) private $: any ) { }

	ngOnInit(): void {
		var reference: any = this.$("#app-body");
		var popper: any = this.$("#example10popper1");
		var s: Popper = new Popper(reference, popper, {
			placement: "right"
		});
	}

	SayToast(): void {
		this.toastr.success("I want to tell a story!");
	}
}
