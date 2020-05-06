import { IGalleryModel } from "../../../ndm";
import { PrototypeElement } from "../../../prototype-lib/index";
import "./image-grid.element.mobile-height.scss";
import "./image-grid.element.mobile-width.scss";
import "./image-grid.element.scss";
export declare class ImageGridElement extends PrototypeElement {
    private model;
    private imageSlider;
    constructor(model: IGalleryModel);
    private PreloadImage;
}
