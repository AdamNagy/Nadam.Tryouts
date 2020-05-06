import { PrototypeElement } from "../../../prototype-lib/prototype-element";
import { CarouselConfig } from "./carouse.config";
import "./carousel.element.scss";
export declare class CarouselElement extends PrototypeElement {
    constructor();
    WithItem(item: HTMLElement): CarouselElement;
    WithImage(src: string): CarouselElement;
    Init(config: CarouselConfig): CarouselElement;
}
