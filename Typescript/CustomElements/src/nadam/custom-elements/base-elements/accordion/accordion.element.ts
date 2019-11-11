import { PrototypeHTMLElement } from "../../../html-extensions/prototype-html-element";
import { AccordionItemElement } from "./accordion-item.element";

export class AccordionElement extends PrototypeHTMLElement {

	private accordionId: string;
	private numOfItems = 0;
	get AccordionId() {
		return this.accordionId;
	}

	constructor(id: string) {
		super();
		this.accordionId = id;

		this.WithClass("accordion")
			.WithAttribute("id", id);
	}

	public WithItem(item: HTMLElement) {

		++(this.numOfItems);
		const accordionItem = new AccordionItemElement(this.accordionId, this.numOfItems.toString(), item);
		this.appendChild(accordionItem);
		return this;
	}
}

customElements.define("ndm-accordion", AccordionElement);
