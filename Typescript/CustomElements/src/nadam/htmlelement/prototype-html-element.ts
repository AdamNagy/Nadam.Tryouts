export class PrototypeHTMLElement extends HTMLElement {

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

	public WithInnerText(text: string) {

		this.innerText = text;
		return this;
	}

	public WithId(id: string) {
		this.setAttribute("id", id);
		return this;
	}
}

customElements.define("ndm-prototype-element", PrototypeHTMLElement);
