
class CarouselElement extends HTMLElement {
	
	constructor(config) {
		super();
  
		this.classList.add("position-relative");
		this.classList.add("d-block");
		config = config || {};

		var elementBody = document.createElement("div");
			
		config.imgSrc = this.getAttribute("img-src") || config.imgSrc;
		config.height = this.getAttribute("height") || config.height;
		
		var imageElement;
		if( config.imgSrc ) {
			imageElement = new Image();
			imageElement.src = config.imgSrc;
		} else if( this.querySelector("picture") ) {
			imageElement = this.querySelector("picture");
		} else if( this.querySelector("img") ) {
			imageElement = this.querySelector("img");
		}

		if(imageElement) {
			imageElement.classList.add("jarallax-img");
			elementBody.appendChild(imageElement);
		}
		
		elementBody.setAttribute("data-jarallax", true);
		elementBody.setAttribute("data-speed", "0.2");
		elementBody.classList.add("jarallax");
		elementBody.classList.add("ndm-jarallax");
		elementBody.style.width = "100%";
		elementBody.style.height = `${config.height}px`;		
		
		jarallax(elementBody, {
			speed: 0.2
		});

		var frontLayer = this.querySelector(".front-layer");
		if(frontLayer) {
			frontLayer.style.zIndex = '1';
		}

		this.append(elementBody);
	}

	withFrontLayer() {

	} 
}

customElements.define('ndm-carousel', CarouselElement);