class CustomElement extends HTMLElement {

	// get text() {
		// return this.getAttribute('text');
	// }

	// set text(newText) {
		// this.wrapper.innerHTML = this.text;
		// this.setAttribute('value', newText);
	// }

	static get observedAttributes() {
		return ['text'];
	} 

	wrapper = document.createElement('span');
	templateId = "template_custom-element";
	myHeader = {};

	constructor() {
		super();
    
		const template = document.getElementById(this.templateId);
		const node = document.importNode(template.content, true);
	
		var shadow = this.attachShadow({mode: 'open'});	 
		shadow.appendChild(node);
	}
	
	setModel() {
		
	}
	
	addSlidePage(slidePageContentElement) {
		
	}
	
	// attributeChangedCallback(name, oldValue, newValue) {
		// console.log(`${oldValue} - ${newValue}`);
		// this[name] = newValue;
		// this.wrapper.innerHTML = this.text;
	// }
}

customElements.define('custom-element', CustomElement);