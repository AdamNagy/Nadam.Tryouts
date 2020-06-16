class HeaderElementConfig {
	Parallax = false;
	Carouse = false;
	CarouseConfig = {};
	Images = [];
	Title = "";
}

class HeaderElement extends HTMLParagraphElement {
	
	constructor() {
		super();
  
		// check if have any children defined or any attribute

		// if no, lets build up the element
		var body = document.createElement("div");

	}
}

customElements.define('ndm-header', HeaderElement);