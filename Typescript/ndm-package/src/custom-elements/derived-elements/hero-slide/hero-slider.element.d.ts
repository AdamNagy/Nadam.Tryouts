import { PrototypeHTMLElement } from "../../../htmlelement/prototype-html-element";
import "./hero-slider.element.scss";
export declare class HeroSlider extends PrototypeHTMLElement {
    private templateId;
    private carousel;
    private mainTitle;
    private subTitle;
    constructor();
    WithImage(src: string): this;
    WithTitle(title: string): this;
    WithSubtitle(subtitle: string): this;
    Init(): HeroSlider;
}
