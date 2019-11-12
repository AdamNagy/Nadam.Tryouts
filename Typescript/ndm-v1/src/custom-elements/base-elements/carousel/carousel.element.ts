import "slick-carousel";
import { PrototypeHTMLElement } from "../../../htmlelement/prototype-html-element";
import "./carousel.element.scss";

export class CarouselElement extends PrototypeHTMLElement {

	constructor() {
		super();
	}

	public WithItem(item: HTMLElement): CarouselElement {
		const itemContainer = document.createElement("div");
		const img = $(item).attr("src");

		$(itemContainer).css({
			backgroundImage: "url(" + img + ")",
			backgroundPosition: "center center",
			backgroundSize: "cover",
		});

		this.appendChild(itemContainer);
		return this;
	}

	public WithImage(src: string): CarouselElement {
		const itemContainer = document.createElement("div");

		$(itemContainer).css({
			backgroundImage: "url(" + src + ")",
			backgroundPosition: "center center",
			backgroundSize: "cover",
		});

		this.appendChild(itemContainer);
		return this;
	}

	public Init(): CarouselElement {
		($(this) as any).slick({
			autoplay: true,
			autoplaySpeed: 3000,
			dots: true,
			infinite: true,
			slidesToScroll: 1,
			slidesToShow: 1,
		});

		return this;
	}
}

customElements.define("ndm-carousel", CarouselElement);
