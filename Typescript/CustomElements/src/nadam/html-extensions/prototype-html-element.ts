export class PrototypeHTMLElement extends HTMLElement {

	constructor() {

		super();
	}

	public WithAttribute(key: string, value: string): PrototypeHTMLElement {

		this.setAttribute(key, value);
		return this;
	}

	public WithStyle(key: any, value: string): PrototypeHTMLElement {

		this.style[key] = value;
		return this;
	}

	public WithClass(name: string): PrototypeHTMLElement {
		this.classList.add(name);
		return this;
	}

	public WithInnerText(text: string): PrototypeHTMLElement {
		this.innerText = text;
		return this;
	}
}

customElements.define("ndm-prototype-element", PrototypeHTMLElement);
