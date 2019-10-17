class CarouselComponent extends HTMLElement {
	
	TemplateId = "template-caruseul";
	CarouselIndicators; // <li data-target="#carouselExampleCaptions" data-slide-to="1"></li>
	CarouselItems;
	
	ImageSources = [];
	
	constructor() {
		super();
		
		const template = document.getElementById(this.TemplateId);
		const node = document.importNode(template.content, true);
		
		this.CarouselItems = node.querySelector("div.carousel-inner");
		this.append(node);
	}
	
	SetImageSources(imgSrcs) {
		this.ImageSources = imgSrcs;
		this.RenderCaruselItems();
	}
	
	RenderCaruselItems() {
		
		for(var i = 0; i < this.ImageSources.length; ++i) {
			let caruselItem = new CarouselItemComponent(this.ImageSources[i]);
			this.CarouselItems.append(caruselItem);
		}
	}	
	
}
customElements.define('nadam-carusel', CarouselComponent);

class CarouselItemComponent extends HTMLElement {
	
	TemplateId = "template-caruseul-item";
	ImageSource = "";
	ImageElement;
	
	constructor(imageSource) {
		super();
		
		this.ImageSource = imageSource;
		
		const template = document.getElementById(this.TemplateId);
		const node = document.importNode(template.content, true);
		
		this.ImageElement = node.querySelector("img");
		this.ImageElement.setAttribute("src", this.ImageSource);
		
		this.append(node);
	}
	
}
customElements.define('nadam-carusel-item', CarouselItemComponent);