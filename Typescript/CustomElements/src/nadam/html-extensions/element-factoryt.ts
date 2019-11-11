import { WrapperHTMLElement } from "../html-extensions/wrapper-html-element";
export class ElementFactory {

	public static CreatePrimitive(elementName: string): WrapperHTMLElement {

		return new WrapperHTMLElement(document.createElement(elementName));
	}

	public static ConstructFromTemplate(tamplateId: string): any {
		const template = document.getElementById(tamplateId);
		return document.importNode((template as any).content, true);
	}
}
