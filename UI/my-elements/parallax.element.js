
class ParallaxElement extends HTMLElement {
	
	constructor(config) {
		super();
  
		config = config || {};

		var self = this;		
		config.src = this.getAttribute("src") || config.src;
		config.height = this.getAttribute("height") || config.height;
		config.width = this.getAttribute("width") || config.width;
		config.maxWidth = this.getAttribute("max-width") || config.width;

		var img = new Image();
		img.src = config.src;
		img.addEventListener("load", (event) => {

			var widthHeightRatio = event.target.width / event.target.height;

			self.style.height = `${config.height || event.target.height}px`;
			var calculatedWidth = (config.height || event.target.height) * widthHeightRatio;
			self.style.width = `${config.width || calculatedWidth}px`;

			// self.style.maxWidth = "300px";
			self.style.backgroundImage = `url(${config.src})`;
			self.style.backgroundPosition = "center"; /* Center the image */
			self.style.backgroundRepeat = "no-repeat"; /* Do not repeat the image */
			self.style.backgroundSize = (config.height || event.target.height) > calculatedWidth ? "contain" : "cover"; /* Resize the*/
			self.style.backgroundAttachment = "local";
			self.style.display = "block";
		});
	}
}

customElements.define('ndm-parallax', ParallaxElement);