export class WrapperHTMLElement {

	public element: HTMLElement;
	constructor(element: HTMLElement) {

		this.element = element;
	}

	public WithAttribute(key: string, value: string): WrapperHTMLElement {

		this.element.setAttribute(key, value);
		return this;
	}

	public WithStyle(key: any, value: string): WrapperHTMLElement {

		this.element.style[key] = value;
		return this;
	}

	public WithClass(name: string): WrapperHTMLElement {
		this.element.classList.add(name);
		return this;
	}

	public WithInnerText(text: string): WrapperHTMLElement {
		this.element.innerText = text;
		return this;
	}
}
