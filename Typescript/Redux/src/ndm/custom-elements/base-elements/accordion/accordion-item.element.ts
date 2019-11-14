import { PrototypeHTMLElement } from "../../../htmlelement/prototype-html-element";

export class AccordionItemElement extends PrototypeHTMLElement {

	private templateId = "template-accordion-item";
	private header: HTMLElement;
	private body: HTMLElement;

	constructor(
		parentId: string,
		index: string,
		child: HTMLElement,
		title: string = "") {

		super();

		const template = document.getElementById(this.templateId);
		const node = document.importNode((template as any).content, true);

		const headerId = `${parentId}-header-${index}`;
		const bodyId = `${parentId}-body-${index}`;

		this.WithClass("card");

		// accordion button (button)
		this.header = node.querySelector("div.card-header")
			.WithId(headerId)
			.WithAttribute("data-target", `#${bodyId}`)
			.WithAttribute("aria-controls", bodyId)
			.WithStyle("cursor", "pointer");

		const headerTitle = (this.header.querySelector("h3") as any)
			.WithInnerText(title);

		// accordion body (content)
		this.body = node.querySelector("div.collapse")
			.WithId(bodyId)
			.WithAttribute("data-parent", `#${parentId}`)
			.WithAttribute("aria-labelledby", headerId);

		const itemContainer = this.body.querySelector("div.card-body");
		itemContainer.appendChild(child);

		this.appendChild(node);
	}

	public Open() {
		this.header.setAttribute("aria-expanded", "true");
		this.body.classList.add("show");
	}
}

customElements.define("ndm-accordion-item", AccordionItemElement);
