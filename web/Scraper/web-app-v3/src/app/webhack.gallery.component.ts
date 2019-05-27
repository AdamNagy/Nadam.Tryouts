import { FtpHelper } from "./webhack.ftp-helper.lib";
import { RemovableElement } from "../nadam/nadam.removable.element";

export class GalleryComponent { 

	constructor(_galleryModel) {

		this.model = _galleryModel;
		this.imagePromises = new Array();

        this.view = document.createElement("div");
        this.view.classList.add("grid");

        for (var i = 0; i < this.model.ImagesMetaData.length; ++i) {

			var imgPromise = (function(imgSrc) {
				return new Promise(function(resolve, reject) {

					var newImage = new Image();
					newImage.onload = () => {resolve(newImage)};
					newImage.src = imgSrc;
					newImage.onerror = () => {resolve()};
				})
				
			})(this.model.ImagesMetaData[i].ThumbnailImageSrc);
			this.imagePromises.push(imgPromise);
		}

		var masterPromise = Promise.all(this.imagePromises)
		masterPromise.then((values) => {this.allImagesOnload(values);});
	}    

	allImagesOnload(imageElements) {

		this.hacker = FtpHelper.GetFtpHandler(this.model.ImagesMetaData);

		for (var i = 0; i < imageElements.length; ++i) {

			if( imageElements[i] !== undefined ) {
				var removable = new RemovableElement(imageElements[i]);
				removable.view.classList.add("grid-item");
				this.view.append(removable.view);
			}		
		}
		
        if (this.hacker.ftpHandler !== undefined) {
			
			for (var i = 0; i < imageElements.length; ++i) {
				if( imageElements[i] !== undefined )
					imageElements[i].setAttribute("real-src", this.hacker.ftpHandler(imageElements[i].getAttribute('src')));				
			}
		}
	}
}