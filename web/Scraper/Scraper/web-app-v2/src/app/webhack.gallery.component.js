import { FtpHelper } from "./webhack.ftp-helper.lib";
import { RemovableElement } from "./../nadam/nadam.removable.element";

export class GalleryComponent {   

	constructor(_galleryModel) {

		this.model = _galleryModel;
		this.item = document.createElement("div");
        this.item.classList.add("d-flex");
        this.item.classList.add("justify-content-center");
        this.item.classList.add("flex-wrap");

		for (var i = 0; i < this.model.ImagesMetaData.length; ++i) {
			var newImg = document.createElement("img");
			// newImg.style.display = "inline";
			newImg.setAttribute("src", this.model.ImagesMetaData[i].ThumbnailImageSrc);
			newImg.setAttribute("data-image-link-href", this.model.ImagesMetaData[i].LinkHref);
			newImg.classList.add("p-1");
			var removable = new RemovableElement(newImg);
			this.item.append(removable.item);
		}

        this.hacker = FtpHelper.GetFtpHandler(this.model.ImagesMetaData);

        if (this.hacker.ftpHandler !== undefined) {

            let hackPromise = this.HackImages(this.model.ImagesMetaData, this.hacker);

            hackPromise.then(
                () => {
                    for (var i = 0; i < 10; i++) {
                        console.log(this.model.ImagesMetaData[i].RealImageSrc);
                    }
                }
            );
        }
	}    

    async HackImages(imagesMetaData, hacker) {
        return new Promise(function (resolve, reject) {
            for (var i = 0; i < imagesMetaData.length; i++) {
                imagesMetaData[i].RealImageSrc
                    = hacker.ftpHandler(imagesMetaData[i].ThumbnailImageSrc);
        }
        resolve();
    });
    }
}