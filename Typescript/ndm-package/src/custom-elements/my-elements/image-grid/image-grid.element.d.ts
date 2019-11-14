import { PrototypeHTMLElement } from "../../../htmlelement/prototype-html-element";
import { GalleryModel } from "./gallery.model";
import "./image-grid.element.mobile.scss";
import "./image-grid.element.scss";
export declare class ImageGrid extends PrototypeHTMLElement {
    config: {
        rootMargin: string;
        threshold: number;
    };
    observer: IntersectionObserver;
    private templateId;
    private itemTemplateId;
    private model;
    private imageViewer;
    private blurLayer;
    private imageMetas;
    private imageContainer;
    constructor(model: GalleryModel);
    WithModel(gallery: GalleryModel): ImageGrid;
    private SlideShow;
    private GetViewImageSrcFor;
    private PreloadImage;
}
