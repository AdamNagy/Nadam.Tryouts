var Thumbail = {

    GetImageSrc: function(thumbnail) {
        var div = thumbnail.firstElementChild;
        var image = div.firstElementChild;

        if (div == null || image == null) {
            return false;
        }

        return image.getAttribute("src");
    },

    ToDownloadable: function(thumbnail, generator) {
        thumbnail.removeAttribute("onclick");

        var imageData;
        if (generator === null) {
            imageData = Thumbail.GetDefaultImageData(thumbnail);
        } else {
            imageData = generator(Thumbail.GetImageSrc(thumbnail));
        }

        thumbnail.setAttribute("download", imageData.Title);
        thumbnail.setAttribute("src", imageData.NewSrc);

        return thumbnail;
    },

    GetDefaultImageData: function(thumbail) {

        var oldImageSrc = Thumbail.GetImageSrc(thumbail);
        var title = oldImageSrc.split("/").Last().split(".").Last();
        return {
            Title: title,
            NewSrc: oldImageSrc
        };
    }
}