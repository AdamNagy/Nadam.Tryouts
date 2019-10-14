class CustomElement extends HTMLElement {

	get text() {
		return this.getAttribute('text');
	}

	set text(newText) {
		this.wrapper.innerHTML = this.text;
		this.setAttribute('value', newText);
	}

	static get observedAttributes() {
		return ['text'];
	}

	wrapper = document.createElement('span');

	constructor() {
		super();
    
		console.log("custom element ctor");
		var shadow = this.attachShadow({mode: 'open'});
		 
		if(this.hasAttribute('text')) {
			this.wrapper.innerHTML = this.text;
		}

		shadow.appendChild(this.wrapper);
	}
	
	attributeChangedCallback(name, oldValue, newValue) {
		console.log(`${oldValue} - ${newValue}`);
		this[name] = newValue;
		this.wrapper.innerHTML = this.text;
	}
}

customElements.define('custom-element', CustomElement);