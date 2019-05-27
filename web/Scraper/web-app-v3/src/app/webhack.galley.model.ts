export class IGalleryModel {

	SourceUrl: string = "";
	Title: string = "";
	ImagesMetaData: Array<string> = new Array();
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

export class GalleryThumbnailModel {

	SourceUrl: string = "";
	Title: string = "";
	ThumbnailImageSources: Array<string> = new Array();
}