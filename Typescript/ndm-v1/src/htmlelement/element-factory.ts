import { PrototypeHTMLElement } from "./prototype-html-element";

export class ElementFactory {

	public static Create(elementName: string): PrototypeHTMLElement {

		return document.createElement(elementName) as PrototypeHTMLElement;
	}

	public static ConstructFromTemplate(tamplateId: string): any {
		const template = document.getElementById(tamplateId);
		return document.importNode((template as any).content, true);
	}
}
