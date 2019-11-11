import "slick-carousel";
import "./carousel.element.scss";

export class CarouselElement extends HTMLElement {

	private templateId = "template-carousel";

	constructor() {
		super();

		const template = document.getElementById(this.templateId);
		const node = document.importNode((template as any).content, true);

		this.appendChild(node);
	}

	public Init(): void {
		($(this) as any).slick({
			infinite: true,
			slidesToScroll: 1,
			slidesToShow: 1,
		});
	}
}

customElements.define("nadam-carousel", CarouselElement);
