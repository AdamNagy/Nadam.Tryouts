import { Component} from "@angular/core";
import { FooterComponent } from "./footer/footer.component";

@Component({
  selector: "app",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"]
})
export class AppComponent {

	footer: FooterComponent;

	addFooter(_footer: FooterComponent): void {
		this.footer = _footer;
	}
}
