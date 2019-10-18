class CarouselComponent extends HTMLElement {
	
	TemplateId = "template-caruseul";
	CarouselIndicators; // <li data-target="#carouselExampleCaptions" data-slide-to="1"></li>
	CarouselItemsContainer;
	
	ImageSources = [];
	
	constructor() {
		super();
		
		const template = document.getElementById(this.TemplateId);
		const node = document.importNode(template.content, true);
		
		this.CarouselItemsContainer = node.querySelector("div.carousel-inner");
		this.append(node);
	}
	
	SetImageSources(imgSrcs) {
		this.ImageSources = imgSrcs;
		this.RenderCaruselItems();
	}
	
	RenderCaruselItems() {
		
		for(var i = 0; i < this.ImageSources.length; ++i) {
			let caruselItem = new CarouselItemComponent(this.ImageSources[i]);
			this.CarouselItemsContainer.append(caruselItem.View);
		}
		
		this.CarouselItemsContainer.querySelector("div.carousel-item").classList.add("active");
	}	
	
}
customElements.define('nadam-carusel', CarouselComponent);

class CarouselItemComponent {
	
	TemplateId = "template-caruseul-item";
	ImageSource = "";
	ImageElement;
	CaruselItemDiv;
	View;
	
	constructor(imageSource) {
		// super();
		
		this.ImageSource = imageSource;
		
		const template = document.getElementById(this.TemplateId);
		const node = document.importNode(template.content, true);
		
		this.ImageElement = node.querySelector("img");
		this.ImageElement.setAttribute("src", this.ImageSource);
		this.CaruselItemDiv	= node.querySelector("div.carousel-item");
	
		this.View = node;
		// this.append(node);
	}
	
	Activate() {
		this.CaruselItemDiv.classList.add("active");
	}
}