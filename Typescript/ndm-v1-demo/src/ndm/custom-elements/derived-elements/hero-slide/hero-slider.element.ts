import { PrototypeHTMLElement } from "../../../htmlelement/prototype-html-element";
import { CarouselElement } from "../../base-elements/carousel/carousel.element";
import "./hero-slider.element.scss";

export class HeroSlider extends PrototypeHTMLElement {

	private templateId = "template-hero-slider";
	private carousel: CarouselElement;
	private mainTitle: PrototypeHTMLElement;
	private subTitle: PrototypeHTMLElement;

	constructor() {

		super();

		// <carousel>
		this.carousel = new CarouselElement();
		this.appendChild(this.carousel);
		// </carousel>

		const template = document.getElementById(this.templateId);
		const node = document.importNode((template as any).content, true);

		// <front-layer>
		const frontLayer = node.querySelector("div.front-layer");
		this.mainTitle = frontLayer.querySelector("span.main-title");
		this.subTitle = frontLayer.querySelector("span.sub-title");
		this.appendChild(frontLayer);
		// <front-layer>
	}

	public WithImage(src: string) {
		this.carousel.WithImage(src);
		return this;
	}

	public WithTitle(title: string) {

		this.mainTitle.WithInnerText(title);
		return this;
	}

	public WithSubtitle(subtitle: string) {

		this.subTitle.WithInnerText(subtitle);
		return this;
	}

	public Init(): HeroSlider {
		this.carousel.Init();
		return this;
	}
}

customElements.define("ndm-hero-slider", HeroSlider);
