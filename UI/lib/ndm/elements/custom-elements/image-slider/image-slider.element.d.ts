import { PrototypeElement } from "../../../prototype-lib/index";
import "./image-slider.element.scss";
export declare class ImageSliderElement extends PrototypeElement {
    private imageSources;
    private currentIdx;
    constructor(images: string[]);
    Open(idx: number): this;
    private Close;
    private NextImage;
    private PrevImage;
}
