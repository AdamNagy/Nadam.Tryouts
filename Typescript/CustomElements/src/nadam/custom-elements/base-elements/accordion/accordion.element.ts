import { PrototypeHTMLElement } from "../../../htmlelement/prototype-html-element";
import { AccordionItemElement } from "./accordion-item.element";

export class AccordionElement extends PrototypeHTMLElement {

	private numOfItems = 0;
	get AccordionId() {
		return this.getAttribute("id");
	}

	private accordions: AccordionItemElement[] = [];

	constructor(id: string) {

		super();

		this.WithId(id)
			.WithStyle("display", "block")
			.WithClass("accordion");
	}

	public WithItem(item: HTMLElement, title: string = "") {

		++(this.numOfItems);
		const accordionItem = new AccordionItemElement(this.AccordionId, this.numOfItems.toString(), item, title);
		this.accordions.push(accordionItem);
		this.appendChild(accordionItem);
		return this;
	}

	public OpenAccordion(idx: number) {
		this.accordions[0].Open();
		return this;
	}
}

customElements.define("ndm-accordion", AccordionElement);
