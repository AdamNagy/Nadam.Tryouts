import { FtpHelper } from "./webhack.ftp-helper.lib";
import { RemovableElement } from "./../nadam/nadam.removable.element";

export class GalleryComponent {   



	constructor(_galleryModel) {

		this.model = _galleryModel;
		this.imagePromises = new Array();

        this.view = document.createElement("div");
        this.view.classList.add("grid");
        // this.view.classList.add("d-flex");
        // this.view.classList.add("justify-content-center");
        // this.view.classList.add("flex-wrap");

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

		for (var i = 0; i < imageElements.length; ++i) {

			if( imageElements[i] !== undefined ) {
				var removable = new RemovableElement(imageElements[i]);
				removable.view.classList.add("grid-item");// removable.view.classList.add("p-1");
				this.view.append(removable.view);
			}		
		}

		$('.grid').masonry({
			itemSelector: '.grid-item',
			columnWidth: 180,
		});
		
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