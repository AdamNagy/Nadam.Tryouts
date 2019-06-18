class CustomElement extends HTMLElement {

	constructor(child) {
		
		super();		
		this.wrapper = document.createElement('span');
		this.shadow = this.attachShadow({mode: 'open'});

		this.wrapper.setAttribute('class','wrapper');
		this.wrapper.innerText = "Static text from ctor";

		this.shadow.appendChild(this.wrapper);
		if( child !== undefined )
			this.shadow.appendChild(child);
	}

	append = function(element) {

		console.log("appending from overridden func");
		this.wrapper.append(element);
	}
}


(function(){

	customElements.define('custom-element', CustomElement);
})()