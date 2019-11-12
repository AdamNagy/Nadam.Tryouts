import { PrototypeHTMLElement } from "../../../htmlelement/prototype-html-element";

class ModalElement extends PrototypeHTMLElement {

	private TemplateId = "template-modal";
	private Body: HTMLElement;
	private Title: HTMLElement;

	constructor() {
		super();

		const template = document.getElementById(this.TemplateId);
		const node = document.importNode((template as any).content, true);

		this.Body = node.querySelector("div.modal-body");
		this.Title = node.querySelector("h5.modal-title");
		this.appendChild(node);
	}

	public WithContent(title: string, bodyContent: HTMLElement) {
		if ( this.Body.firstChild )
			this.Body.firstChild.remove();

		this.Body.append(bodyContent);
		this.Title.innerText = title;

		return this;
	}
}

customElements.define("ndm-modal", ModalElement);
export const Modal = new ModalElement();
