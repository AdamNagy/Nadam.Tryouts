import { FtpHelper } from "./webhack.ftp-helper.lib";
import { RemovableElement } from "../nadam/nadam.removable.element";
import { GalleryModel } from "./webhack.galley.model";



export class GalleryComponent {

	Model: GalleryModel;
	ImagePromises: Array<Promise<any>> = new Array();
	View = document.createElement("div");

	constructor(_galleryModel: GalleryModel) {

		this.Model = _galleryModel;
        this.View.classList.add("grid");

        for (var i: number = 0; i < this.Model.ImagesMetaData.length; ++i) {

			var imgPromise: Promise<any> = (function(imgSrc: string): any {
				let promise: Promise<any> = new Promise(function(resolve: any, reject: any): any {

					var newImage: HTMLImageElement = new Image();
					newImage.onload = () => {resolve(newImage);};
					newImage.src = imgSrc;
					newImage.onerror = () => {resolve();};
				});
			})(this.Model.ImagesMetaData[i].ThumbnailImageSrc);
			this.ImagePromises.push(imgPromise);
		}

		var masterPromise: Promise<any> = Promise.all(this.ImagePromises);
		masterPromise.then((values) => {this.AllImagesOnload(values);});
	}

	private AllImagesOnload(imageElements: Array<HTMLImageElement>): void {

		let hacker: any = FtpHelper.GetFtpHandler(this.Model.ImagesMetaData);

		for (let i: number = 0; i < imageElements.length; ++i) {

			if( imageElements[i] !== undefined ) {
				var removable: RemovableElement = new RemovableElement(imageElements[i]);

				removable.classList.add("grid-item");
				this.View.append(removable);
			}
		}

        if (hacker.ftpHandler !== undefined) {

			for (let i: number = 0; i < imageElements.length; ++i) {
				if( imageElements[i] !== undefined ) {
					imageElements[i].setAttribute("real-src", hacker.ftpHandler(imageElements[i].getAttribute("src")));
				}
			}
		}
	}
}