import { PrototypeHTMLElement } from "../../../htmlelement/prototype-html-element";
import "./removable.element.scss";

export class RemovableElement extends PrototypeHTMLElement {

	private child: HTMLElement;
	private TemplateId = "template-removable";

	get Child() {
		return this.child;
	}

	constructor(item: HTMLElement) {

		super();
		this.child = item;

		const template = document.getElementById(this.TemplateId);
		const view = document.importNode((template as any).content, true);
		const remover: HTMLElement = view.querySelector("div.remover");

		remover.addEventListener("click", () => {
			this.FireOnRemove(this);
			this.remove();
		});

		view.querySelector("div.removable").appendChild(item);
		this.appendChild(view);
	}

	// OnRemove event
	private onRemove: any[] = [];
	public OnRemove(func: any) {
		this.onRemove.push(func);
	}

	private FireOnRemove(element: RemovableElement) {
		for (const func of this.onRemove) {
			func(element);
		}
	}
}

customElements.define("ndm-removable", RemovableElement);
