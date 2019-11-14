import "slick-carousel";
import { PrototypeHTMLElement } from "../../../htmlelement/prototype-html-element";
import "./carousel.element.scss";
export declare class CarouselElement extends PrototypeHTMLElement {
    constructor();
    WithItem(item: HTMLElement): CarouselElement;
    WithImage(src: string): CarouselElement;
    Init(): CarouselElement;
}
