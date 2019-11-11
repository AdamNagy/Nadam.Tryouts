import { PrototypeHTMLElement } from "../../../html-extensions/prototype-html-element";

export class AccordionItemElement extends PrototypeHTMLElement {

	private templateId = "template-accordion-item";

	constructor(parentId: string, index: string, child: HTMLElement) {
		super();

		const template = document.getElementById(this.templateId);
		const node = document.importNode((template as any).content, true);

		const headerId = `${parentId}-header-${index}`;
		const bodyId = `${parentId}-body-${index}`;

		this.WithClass("card");

		// accordion button (button)
		const header = node.querySelector("div.card-header");
		header.setAttribute("id", headerId);
		const opener = header.querySelector("button");
		opener.setAttribute("data-target", `#${bodyId}`);
		opener.setAttribute("aria-controls", bodyId);

		// accordion body (content)
		const body = node.querySelector("div.collapse");
		body.setAttribute("id", bodyId);
		body.setAttribute("data-parent", `#${parentId}`);
		body.setAttribute("aria-labelledby", headerId);
		const itemContainer = body.querySelector("div.card-body");
		itemContainer.appendChild(child);

		this.appendChild(node);
	}
}

customElements.define("ndm-accordion-item", AccordionItemElement);
