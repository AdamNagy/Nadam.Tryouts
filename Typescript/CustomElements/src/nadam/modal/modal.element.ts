class ModalElement extends HTMLElement {
	
	private TemplateId = "modal";
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
	
	SetContent(title: string, bodyContent: HTMLElement) {
		if( this.Body.firstChild )
			this.Body.firstChild.remove();

		this.Body.append(bodyContent);
		this.Title.innerText = title;
	}
}

export class ModalOpenerElement extends HTMLElement {
	
	TemplateId = "modal-opener";
	Button: HTMLElement;
	
	constructor(buttonText: string) {
		super();
		
		const template = document.getElementById(this.TemplateId);
		const node = document.importNode((template as any).content, true);
		
		this.Button = node.querySelector("button");
		this.Button.innerHTML = buttonText || this.getAttribute("buttonText");
		this.appendChild(node);
	}
}


customElements.define('nadam-modal', ModalElement);
customElements.define('nadam-modal-opener', ModalOpenerElement);

export const Modal = new ModalElement();