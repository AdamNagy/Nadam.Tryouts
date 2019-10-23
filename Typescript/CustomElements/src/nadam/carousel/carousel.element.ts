export class CarouselComponent extends HTMLElement {
	
	private TemplateId = "template-caruseul";
	private CarouselIndicators: HTMLElement; // <li data-target="#carouselExampleCaptions" data-slide-to="1"></li>
	private CarouselItemsContainer: HTMLElement;
	private CarouselItems: HTMLElement[] = [];
	
	constructor() {
		super();
		
		const template = document.getElementById(this.TemplateId);
		const node = document.importNode((template as any).content, true);
		
		this.CarouselItemsContainer = node.querySelector("div.carousel-inner");
		this.append(node);
	}
	
	SetCarouselItems(items: HTMLElement[]) {
		this.CarouselItems = items;
		this.RenderCaruselItems();
	}
	
	RenderCaruselItems() {
		
		for(var i = 0; i < this.CarouselItems.length; ++i) {
			let caruselItem = new CarouselItemComponent(this.CarouselItems[i]);
			this.CarouselItemsContainer.append(caruselItem.view);
		}
		
		this.CarouselItemsContainer.querySelector("div.carousel-item").classList.add("active");
	}	
	
}
customElements.define('nadam-carusel', CarouselComponent);

class CarouselItemComponent {
	
	private TemplateId = "template-caruseul-item";
	private ContentContainer: HTMLElement;
	private View: HTMLElement;
	get view() {
		return this.View;
	}

	constructor(content: HTMLElement) {		
		const template = document.getElementById(this.TemplateId);
		const node = document.importNode((template as any).content, true);
		
		this.ContentContainer	= node.querySelector("div.carousel-item");
		this.ContentContainer.appendChild(content);

		this.View = node;
	}
	
	Activate() {
		this.ContentContainer.classList.add("active");
	}
}