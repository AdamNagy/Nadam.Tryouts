export class GalleryModel {

	SourceUrl: string = "";
	Title: string = "";
	ImagesMetaData: Array<ImageMetadataModel> = new Array();
	FtpSite: string = "";

    get Domain(): string {

		let domain: string = "";
        if (this.SourceUrl !== undefined) {
            let splitted: Array<string> = this.SourceUrl.split("/");
            domain = splitted[2];
        }

        return domain;
    }
}

export class ImageMetadataModel {

	LinkHref: string = "";
	ThumbnailImageSrc: string = "";
	RealImageSrc: string = "";
}

export class GalleryThumbnailModel {

	SourceUrl: string = "";
	Title: string = "";
	ThumbnailImageSources: Array<string> = new Array();
}