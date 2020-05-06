import { IGalleryModel } from "../../../models/gallery.model";
import { PrototypeElement } from "../../../prototype-lib";
import { ITilesConfig } from "./unite-gallery.config-interface";
export declare class UniteGalleryElement extends PrototypeElement {
    constructor(gallery: IGalleryModel);
    Init(config: ITilesConfig): void;
}
