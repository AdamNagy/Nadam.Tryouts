// Type: Domain logic
// File name: nadam.urlgalleries.hacker.js
// Class name: UrlGalleriesHacker
// Namespace: Nadam.Webhack

;
"use strict";

window.Nadam = window.Nadam || {};
Nadam.Webhack = Nadam.Webhack || {};

Nadam.Webhack.UrlGalleriesHacker = new function () {

    var galleryHelper = {};

    var GetImageMeta = function (imageElement) {

        var imageMetaData = new Nadam.Webhack.ImageMetaData();

        imageMetaData.thumbnailImageSrc = imageElement.getAttribute("src");
        imageMetaData.thumbnailLinkHref = imageElement.getAttribute("data-image-link-href");

        if (imageMetaData.thumbnailLinkHref.startsWith('/'))
            imageMetaData.thumbnailLinkHref = window.location.origin + imageMetaData.thumbnailLinkHref;

        if (galleryHelper.ftpHandler !== undefined)
            imageMetaData.realImageSrc = galleryHelper.ftpHandler(imageMetaData.thumbnailImageSrc);
    };

    this.HackImages = function (imagesMeta) {

        var imagesMetaData = new Array();
        // var imageElements = section.getElementsByTagName("img");
        galleryHelper = Nadam.Webhack.FtpHelper.GetFtpHandler(imageElements);

        for (var i = 0; i < imagesMeta.length; ++i) {

            var imageMeta = GetImageMeta(imageElements[i]);
            imagesMetaData.push(imageMeta);
        }

        return imagesMetaData;
    };
}