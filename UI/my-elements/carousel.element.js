
class CarouselElement extends HTMLElement {
	
	config = {};
	bodyElement = {};
	slickConfig = {};

	constructor(_config) {
		super();
  
		this.classList.add("position-relative");
		this.classList.add("d-block");

		this.bodyElement = document.createElement("div");

		this.config = _config || {};
		this.config.height = this.getAttribute("height") || _config.height;
		this.config.configVariableName = this.getAttribute("configVariableName") || _config.configVariableName;
		this.config.imageSources = _config && _config.imageSources;

		var imageElements;
		 if( this.querySelector("img") ) {
			imageElements = this.querySelectorAll("img");
		} else if( this.querySelector("ndm-img-wall") ) {
			imageElements = this.querySelectorAll("ndm-img-wall");
		} else if( this.config.imageSources && this.config.imageSources.length && this.config.imageSources.length > 0 ) {
			imageElements = [];
			for(var imgSource of this.config.imageSources ) {
				var imgElement = new Image();
				imgElement.src = imgSource;
				imageElements.push(imgElement);
			}
		}

		for(var imgElement of imageElements ) {
			this.bodyElement.append(imgElement);
		}

		var frontLayer = this.querySelector(".front-layer");
		if(frontLayer) {
			frontLayer.style.zIndex = '1';
		}

		this.slickConfig = window[this.config.configVariableName];
		if( !this.slickConfig )
			this.slickConfig = SingleSlideCarouselConfig;

		this.append(this.bodyElement);
	}

	// runs each time the element is added to the DOM
	connectedCallback() {
		$(this.bodyElement).slick(this.slickConfig);
	}
}

customElements.define('ndm-carousel', CarouselElement);
