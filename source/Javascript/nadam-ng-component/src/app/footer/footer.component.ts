import { Component} from "@angular/core";
import { AppComponent } from "./../app.component";
@Component({
  selector: "nang-footer",
  templateUrl: "./footer.component.html",
  styleUrls: ["./footer.component.css"]
})
export class FooterComponent {

	constructor(parent: AppComponent) {
		parent.addFooter(this);
	}

}
