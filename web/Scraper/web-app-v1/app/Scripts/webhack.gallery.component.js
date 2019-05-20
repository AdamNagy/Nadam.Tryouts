;
"use strict";

window.Nadam = window.Nadam || {}
Nadam.Webhack = Nadam.Webhack || {};

Nadam.Webhack.GalleryComponent = function (galleryModel) {

    var self = this;
    var model;
    var hacker;

    this.item = {};

    self.HackImages = function () {

        return new Promise(function (resolve, reject) {

            if (self.hacker.ftpHandler !== undefined) {
                for (var i = 0; i < self.model.ImagesMetaData.length; i++) {
                    self.model.ImagesMetaData[i].RealImageSrc
                        = self.hacker.ftpHandler(self.model.ImagesMetaData[i].ThumbnailImageSrc)
                }

                resolve();
            }

            reject();
        });
    };

    (function (_galleryModel) {

        self.model = _galleryModel;
        self.item = document.createElement("div");
        self.item.classList.add("d-flex");
        self.item.classList.add("justify-content-center");
        self.item.classList.add("flex-wrap");
        // d-flex justify-content-center

        for (var i = 0; i < self.model.ImagesMetaData.length; ++i) {
            var newImg = document.createElement("img");
            // newImg.style.display = "inline";
            newImg.setAttribute("src", self.model.ImagesMetaData[i].ThumbnailImageSrc);
            newImg.setAttribute("data-image-link-href", self.model.ImagesMetaData[i].LinkHref);
            newImg.classList.add("p-1");
            var removable = new RemovableElement(newImg);
            self.item.append(removable.item);
        }

        self.hacker = Nadam.Webhack.FtpHelper.GetFtpHandler(self.model.ImagesMetaData);
        var hackPromise = self.HackImages();
        hackPromise.then(function () {
            for (var i = 0; i < 10; i++) {
                console.log(self.model.ImagesMetaData[i].RealImageSrc);
            }
        });

    })(galleryModel);
}