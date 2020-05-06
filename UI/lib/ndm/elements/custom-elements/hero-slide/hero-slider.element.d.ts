import { PrototypeElement } from "../../../prototype-lib/index";
import "./hero-slider.element.scss";
export declare class HeroSlider extends PrototypeElement {
    private carousel;
    constructor();
    WithImage(src: string): this;
    WithTitle(title: string): this;
    WithSubtitle(subtitle: string): this;
    Init(): HeroSlider;
}
