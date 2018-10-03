import { Component, Inject, OnInit } from "@angular/core";
import * as legacy from "./../es2015module";

// import { draggable } from "jquery-ui";

@Component({
  selector: "app",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"]
})
export class AppComponent implements OnInit {

	ngOnInit(): void {
		// $(".row").css("background", "red"); // works well
		$("#draggable").draggable();

		console.log("external es2015 module: " + legacy());
	}
}
