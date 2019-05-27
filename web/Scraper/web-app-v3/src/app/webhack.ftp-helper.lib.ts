export class FtpHelper {

    static imagevenueurlHandler(currSrc) {
        // start src: 	http://img175.imagevenue.com/loc176/th_99258_c62b4bbf797da03c44f2f6d148e76411_123_176lo.jpg
        // dest src:	http://img2175.imagevenue.com/aAfkjfp01fo1i-3407/loc176/99258_c62b4bbf797da03c44f2f6d148e76411_123_176lo.jpg

        // [ "http:", "", "img231.imagevenue.com", "loc474", "th_92418_df1043116ec4ec8ce0d15ba80b…" ]

        var urlFragments = currSrc.split("/");
        urlFragments[4] = urlFragments[4].replace("th_", "");
        return "http://" + urlFragments[2] + "/img.php?image=" + urlFragments[4];
    }

    static sexyimgurlHandler(currSrc) {
        return currSrc.replace("small", "big");
    }

    // var sexyimgurlhandler = function(currSrc) {
    // 	return currSrc.replace("small", "big");
    // }

    static imagezillaHandler(currSrc) {
        //  final url:			http://imagezilla.net/images/bboyTIt-33c_0001.jpg
        //  					
        //  thumbnail img src:	http://imagezilla.net/thumbs2/bboyTIt-33c_0001_tn.jpg
        //  image link href:	http://imagezilla.net/show/bboyTIt-33c_0001.jpg

        return currSrc.replace("_tn", "").replace("/thumbs2", "/images");
    }

    static imgdinoHandler(currSrc) {
        // http://img2.imgdino.com/images/40484973034650128355_thumb.jpg
        return currSrc.replace("_thumb", "");
    }

    static imgchiliHandler(currSrc) {
        // http://imgchili.net/show/84965/84965322_ro9_0003.jpg
        // http://i8.imgchili.net/84965/84965322_ro9_0003.jpg
        var um = currSrc.split("/");

        return "http://" + um[2].replace("t8", "i8") + "/" + um[3] + "/" + um[4];
    }

    static pixhostHandler(currSrc) {
        // https://t19.pixhost.to/thumbs/255/70943962_img_0002.jpg
        // https://img19.pixhost.to/images/255/70943962_img_0002.jpg

        // https://t19.pixhost.to/thumbs/255/70943960_img_0001.jpg
        // https://img19.pixhost.to/images/255/70943960_img_0001.jpg

        var um = currSrc.split("/");

        return "https://" + um[2].replace("t", "img") + "/images/" + um[4] + "/" + um[5];
    }

    static fappicHandler(currSrc) {
        // http://fappic.com/small/KZnf.jpg
        var um = currSrc.split("/");
        return "http://fappic.com/" + um[4];
    }

    // var imagetwisthandler = function (currSrc, link, i) {
    // 	// link href: 	https://imagetwist.com/qxnqktiyzdd3/IMG_0001.JPG
    // 	// img src: 	https://img64.imagetwist.com/th/21247/qxnqktiyzdd3.jpg

    // 	// dest
    // 	// img src: https://img64.imagetwist.com/i/21247/qxnqktiyzdd3.JPG/IMG_0001.JPG
    // 	var linkSplitted = link.split('/');
    // 	var currSrcSplitted = currSrc.split('/');

    // 	return " https://" + currSrcSplitted[2] + "/i/" + currSrcSplitted[4] + "/" + currSrcSplitted[5].replace("jpg", "JPG") + "/" +  linkSplitted[4];		
    // }

    static  imagetwistHandler(thumbnailImageSource) {

        // final url:           https://img65.imagetwist.com/i/19425/qbkfz2r7aro8.JPG/IMG_0001.JPG
        // final url 2:			https://img161.imagetwist.com/i/20897/yvsryjdup9wb.jpg/ltZ_0002.jpg
        // 						https://img161.imagetwist.com/th/20897/yvsryjdup9wb.jpg
        // thumbnail img src:   https://img65.imagetwist.com/th/19425/qbkfz2r7aro8.jpg
        // image link href:     https://beautifulteenmodels.urlgalleries.net/porn-picture-76r17thodv.jpg

        var splitted_thumbnailImageSource = thumbnailImageSource.split('/');

        // return "https://" + splitted_thumbnailImageSource[2] + "/i/" + 
        // 		splitted_thumbnailImageSource[4] + "/" + 
        // 		splitted_thumbnailImageSource[5].replace("jpg", "JPG");		

        return thumbnailImageSource.replace("/th/", "/i/").replace("jpg", "jpeg");
    }

    static acidimgHandler(currSrc) {
        // https://acidimg.cc/upload/small/2018/01/15/5a5ce095075b2.JPG
        // https://acidimg.cc/upload/small/2017/07/13/5967ba55bd779.JPG
        // https://i.acidimg.cc/big/2017/07/13/5967ba55bd779.JPG

        return currSrc.replace("small", "big");
    }

    static imagebamHandler(currSrc) {
        // orig src: http://thumbs2.imagebam.com/e8/03/83/d6c3b4959882404.jpg
        //orig link: http://www.imagebam.com/image/d6c3b4959882404
        // dest src: http://images2.imagebam.com/2f/48/5c/d6c3b4959882404.jpg

        return "NO-IMPLEMENTATION";
    }

    static imgboxHandler(currSrc) {
        // input: 	https://thumbs2.imgbox.com/bf/b6/5QO4uGfw_t.jpg
        // output: 	https://images2.imgbox.com/bf/b6/5QO4uGfw_o.jpg	

        return currSrc.replace("thumbs", "images").replace("_t", "_o");
    }

    static imxHandler(currSrc) {

        // orig1:	https://imx.to/upload/small/2017/09/20/59c28f343e85b.JPG
        // dest1:	https://x001.imx.to/i/2017/09/20/59c28f343e85b.JPG

        // orig2: 	https://imx.to/u/t/2018/09/23/1um1n6.jpg
        // dest2: 	https://i.imx.to/i/2018/09/23/1um1n6.jpg

        // case: orig1
        if (currSrc.includes("upload") &&
            currSrc.includes("small")) {

            return currSrc.replace("imx.to", "x001.imx.to").replace("small/", "i/").replace("upload/", "");
        } else {
            return currSrc.replace("imx.to", "i.imx.to").replace("u/", "i/").replace("t/", "");
        }
    }

    static getFtpName(source) {

        if (source.includes("imagevenue"))
            return "imagevenue";

        if (source.includes("sexyimg"))
            return "sexyimg";

        if (source.includes("imagezilla"))
            return "imagezilla";

        if (source.includes("imagetwist"))
            return "imagetwist";

        if (source.includes("imgdino"))
            return "imgdino";

        if (source.includes("img.yt"))
            return "img.yt";

        if (source.includes("imgchili"))
            return "imgchili";

        if (source.includes("pixhost"))
            return "pixhost";

        if (source.includes("fappic"))
            return "fappic";

        if (source.includes("acidimg"))
            return "acidimg";

        if (source.includes("imagebam"))
            return "NO-IMPLEMENTATION";

        if (source.includes("imgbox"))
            return "imgbox";

        if (source.includes("imx"))
            return "imx";

        if (source.includes("imgserve"))
            return "imgserve";

        return "NO-IMPLEMENTATION";
    }

    static GetFtpHandler(imageMetas) {

        let simpleHandler = (imageLinkHref, thumbnailImageSource) => thumbnailImageSource;
        var imageFromMiddle = imageMetas[Math.ceil(imageMetas.length / 2)];
        var imageSource = imageFromMiddle.ThumbnailImageSrc;

        var ftp = FtpHelper.getFtpName(imageSource);

        var meta = { };
        switch (ftp) {
            case "imagevenue":
                meta = { "ftpHandler": simpleHandler, "type": "link" };
                break;
            case "acidimg":
                meta = { "ftpHandler": FtpHelper.acidimgHandler, "type": "image" };
                break;
            case "img.yt":
            case "sexyimg":
            case "imx":
            case "imgserve":
                meta = { "ftpHandler": FtpHelper.imxHandler, "type": "image" };
                break;
            case "imagezilla":
                meta = { "ftpHandler": FtpHelper.imagezillaHandler, "type": "image" };
                break;
            case "imagetwist":
                meta = { "ftpHandler": FtpHelper.imagetwistHandler, "type": "image" };
                break;
            case "imgdino":
                meta = { "ftpHandler": FtpHelper.imgdinoHandler, "type": "image" };
                break;
            case "imgchili":
                meta = { "ftpHandler": FtpHelper.imgchiliHandler, "type": "image" };
                break;
            case "pixhost":
                meta = { "ftpHandler": FtpHelper.pixhostHandler, "type": "image" };
                break;
            case "fappic":
                meta = { "ftpHandler": FtpHelper.fappicHandler, "type": "image" };
                break;
            case "imgbox":
                meta = { "ftpHandler": FtpHelper.imgboxHandler, "type": "image" };
                break;

            default: break;
        }

        meta.ftpSiteName = ftp;
        return meta;
    }
}