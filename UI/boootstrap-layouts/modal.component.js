class ModalComponent extends HTMLElement {
	
	TemplateId = "modal";
	Body: HTMLElement;
	
	constructor() {
		super();
		
		const template = document.getElementById(this.TemplateId);
		const node = document.importNode(template.content, true);
		
		this.appendChild(ndoe);
	}
	
	SetContent(bodyContent) {
		this.Body = bodyContent;
	}
}

customElements.define('nadam-modal', ModalComponent);
export const Modal = new ModalComponent();

export class ModalOpenerComponent extends HTMLElement {
	
	TemplateId = "modal-opener";
	
	constructor() {
		super();
		
		const template = document.getElementById(this.TemplateId);
		const node = document.importNode(template.content, true);
		
		this.appendChild(ndoe);
	}
}

customElements.define('nadam-modal-opener', ModalOpenerComponent);