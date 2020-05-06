export declare class StoredlGalleryModel {
    Title: string;
    ImageTitles: string[];
    Route: string;
}
/**
 * Defined by MIV already downloaded gallery metas
 * So defined by History
 */
export declare class WebGalleryModel implements IGalleryModel {
    Title: string;
    SourceUrl: string;
    ImagesMetaData: Array<ImageMetadataModel>;
}
export declare class ImageMetadataModel {
    thumbnailImageSrc: string;
    realImageSrc: string;
}
export interface IGalleryModel {
    Title: string;
    ImagesMetaData: ImageMetadataModel[];
}
