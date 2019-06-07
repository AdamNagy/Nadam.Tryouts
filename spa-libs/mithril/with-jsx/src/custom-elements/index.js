import { PopUpInfo } from "./popup-info.element";

export class CustomElements {

	static Init() {

		customElements.define('popup-info', PopUpInfo);
	}
}