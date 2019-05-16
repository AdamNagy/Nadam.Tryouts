import { FtpHelper } from "./webhack.ftp-helper.lib";
import { RemovableElement } from "./../nadam/nadam.removable.element";

export class GalleryComponent {   



	constructor(_galleryModel) {

		this.model = _galleryModel;
        this.item = document.createElement("div");
        this.item.classList.add("grid");
        //this.item.classList.add("d-flex");
        //this.item.classList.add("justify-content-center");
        //this.item.classList.add("flex-wrap");

		this.imagePromises = new Array();

        for (var i = 0; i < this.model.ImagesMetaData.length; ++i) {
            var newImg = new Image();// document.createElement("img");
            //newImg.name = this.model.ImagesMetaData[i].ThumbnailImageSrc;
            
            //newImg.setAttribute("src", this.model.ImagesMetaData[i].ThumbnailImageSrc);
			newImg.setAttribute("data-image-link-href", this.model.ImagesMetaData[i].LinkHref);
            // newImg.classList.add("p-1");
            var removable = new RemovableElement(newImg);
            removable.item.classList.add("grid-item");
            newImg.onload = (function (_imgContainer) {
                return function () {
                    if (this.height > this.width)
                        _imgContainer.classList.add("grid-item--portrait");
                    else
                        _imgContainer.classList.add("grid-item--landscape");
                };
            })(removable.item);

            newImg.src = this.model.ImagesMetaData[i].ThumbnailImageSrc;
            this.item.append(removable.item);

			var imgPromise = (function(imgSrc) {
				return new Promise(function(resolve, reject) {

					var newImage = new Image();
					newImage.onload = resolve;
					newImage.src = imgSrc;
					newImage.onfail = reject;
				});
			})(this.model.ImagesMetaData[i].ThumbnailImageSrc);

			this.imagePromises.push(imgPromise);
		}

		// todo: preserve 'this'
		// finish implementation
		Promise.all(this.imagePromises).then(function() {

			
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