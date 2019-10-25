import { AccordionItemElement } from "./accordion-item.element";

export class AccordionElement {

	// private templateId = "template-accordion";
	private accordionId: string;
	private numOfItems = 0;
	get AccordionId() {
		return this.accordionId;
	}

	private view: HTMLElement;
	get View() {
		return this.view;
	}

	constructor(id: string) {
		this.accordionId = id;

		// const template = document.getElementById(this.templateId);
		// const node = document.importNode((template as any).content, true);
		this.view = document.createElement("div");
		this.view.setAttribute("id", id);
		this.view.classList.add("accordion");
	}

	public AddItem(item: HTMLElement) {

		++(this.numOfItems);
		const accordionItem = new AccordionItemElement(this.accordionId, this.numOfItems.toString(), item);
		this.view.appendChild(accordionItem);
	}
}

// customElements.define("nadam-accordion", AccordionElement, { extends: "div" });
