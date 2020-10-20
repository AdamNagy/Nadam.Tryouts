/*
	<div class="modal" id="modal-1">
		<p class="modal-closer h1" id="close-portrait">X</p>
		<div class="modal-container d-flex justify-content-center align-items-center">
			<img class="modal-img" src="../test-images/big/portraits/space_1.jpg">
		</div>
	</div>
*/

class ModalElement extends HTMLElement {

	config = {};
	bodyElement = {};

	constructor(_config) {
		super();

		this.config = _config || {};
		this.config.id = this.getAttribute("id") || _config && _config.id;
		this.config.content = this.children || _config && _config.content;

		var template = `
			<p class="modal-closer h1">X</p>
			<div class="modal modal-container d-flex justify-content-center align-items-center">
			</div>
		`;

		let domparser = new DOMParser();
		var bodyElementDocument = domparser.parseFromString(template, "text/html");
		this.bodyElement = bodyElementDocument.body.children;
	}

	// runs each time the element is added to the DOM
	connectedCallback() {
		// this.classList.add("modal");

		if( this.config.content ) {
			if( this.config.content.length !== undefined ) {

				while( this.config.content.length > 0 ) {

					this.bodyElement[1].append(this.config.content[0]);
				}
			} else {
				this.bodyElement[1].appendChild(this.config.content);
			}
		}
	
		this.append(this.bodyElement[0]);
		this.append(this.bodyElement[0]);
		

		this.querySelector(".modal-closer").addEventListener("click", (clickEcent) => {
			$(this).hide();
		});
	}
}

customElements.define('ndm-modal', ModalElement);