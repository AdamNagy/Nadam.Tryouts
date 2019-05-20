;
"use strict";

window.Nadam = window.Nadam || {}
Nadam.Webhack = Nadam.Webhack || {};

Nadam.Webhack.ImageMetaData = function () {

    this.LinkHref = "";
    this.ThumbnailImageSrc = "";
    this.RealImageSrc = "";
}

Nadam.Webhack.GalleryModel = function () {

    this.SourceUrl = "";
    this.Title;
    this.ImagesMetaData = new Array();
    this.FtpSite = "";

    var self = this; 

    var id;
    this.Id = function () {

    }

    var domain;
    this.Domain = function () {

        if (doain == undefined) {
            var splitted = self.SourceUrl.split('/');
            domain = splitted[2];
        }

        return domain;
    }
}