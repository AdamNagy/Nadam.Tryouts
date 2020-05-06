import { PrototypeElement } from "../../../prototype-lib/prototype-element";
import "./parallax.element.scss";
export declare class ParallaxElement extends PrototypeElement {
    constructor(background: string, height?: number, scrollSpeed?: number);
    WithHeight(height: number): ParallaxElement;
}
