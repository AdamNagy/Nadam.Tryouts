
class ParallaxElement extends HTMLElement {
	
	constructor(config) {
		super();
  
		this.classList.add("position-relative");
		this.classList.add("d-block");
		config = config || {};

		var bodyElement = document.createElement("div");
			
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
			bodyElement.appendChild(imageElement);
		}
		
		bodyElement.setAttribute("data-jarallax", true);
		bodyElement.setAttribute("data-speed", "0.2");
		bodyElement.classList.add("jarallax");
		bodyElement.classList.add("ndm-jarallax");
		bodyElement.style.width = "100%";
		bodyElement.style.height = `${config.height}px`;		
		
		jarallax(bodyElement, {
			speed: 0.2
		});

		var frontLayer = this.querySelector(".front-layer");
		if(frontLayer) {
			frontLayer.style.zIndex = '1';
		}

		this.append(bodyElement);
	}
}

customElements.define('ndm-parallax', ParallaxElement);