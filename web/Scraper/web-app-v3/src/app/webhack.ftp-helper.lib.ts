import { ImageMetadataModel } from "./webhack.galley.model";

export class GalleryMetaData {

	FtpSiteName: FtpSite;
	Type: string;
	FtpHandler: any;
}

export enum FtpSite {
	imagevenue,
	acidimg,
	"img.yt",
	sexyimg,
	imx,
	imgserve,
	imagezilla,
	imagetwist,
	imgdino,
	imgchili,
	pixhost,
	fappic,
	imgbox,
	"NO-IMPLEMENTATION"
}

export class FtpHelper {

    public static GetFtpHandler(imageMetas: ImageMetadataModel[]): GalleryMetaData {

        let simpleHandler = (imageLinkHref: string, thumbnailImageSource: string) => thumbnailImageSource;
        var imageFromMiddle = imageMetas[Math.ceil(imageMetas.length / 2)];
        var imageSource = imageFromMiddle.ThumbnailImageSrc;

        var ftp = FtpHelper.getFtpName(imageSource);

        var meta = new GalleryMetaData();
        switch (ftp) {
            case FtpSite.imagevenue:
				meta.FtpHandler = simpleHandler;
				meta.Type = "link";
                break;
            case FtpSite.acidimg:
				meta.FtpHandler = FtpHelper.acidimgHandler; 
				meta.Type = "image";
                break;
            case FtpSite["img.yt"]:
            case FtpSite.sexyimg:
            case FtpSite.imx:
            case FtpSite.imgserve:
				meta.FtpHandler = FtpHelper.imxHandler; 
				meta.Type ="image";
                break;
            case FtpSite.imagezilla:
				meta.FtpHandler = FtpHelper.imagezillaHandler;
				meta.Type = "image";
                break;
            case FtpSite.imagetwist:
				meta.FtpHandler = FtpHelper.imagetwistHandler; 
				meta.Type = "image";
                break;
            case FtpSite.imgdino:
				meta.FtpHandler = FtpHelper.imgdinoHandler; 
				meta.Type = "image";
                break;
            case FtpSite.imgchili:
				meta.FtpHandler = FtpHelper.imgchiliHandler; 
				meta.Type = "image";
                break;
            case FtpSite.pixhost:
				meta.FtpHandler = FtpHelper.pixhostHandler;
				meta.Type = "image";
                break;
            case FtpSite.fappic:
				meta.FtpHandler = FtpHelper.fappicHandler;
				meta.Type = "image";
                break;
            case FtpSite.imgbox:
				meta.FtpHandler = FtpHelper.imgboxHandler;
				meta.Type = "image";
                break;
			default: 				
				meta.FtpHandler = simpleHandler;
				meta.Type = "link";
				break;
        }

        meta.FtpSiteName = ftp;
        return meta;
    }

    private static imagevenueurlHandler(currSrc: string): string {
        // start src: 	http://img175.imagevenue.com/loc176/th_99258_c62b4bbf797da03c44f2f6d148e76411_123_176lo.jpg
        // dest src:	http://img2175.imagevenue.com/aAfkjfp01fo1i-3407/loc176/99258_c62b4bbf797da03c44f2f6d148e76411_123_176lo.jpg

        // [ "http:", "", "img231.imagevenue.com", "loc474", "th_92418_df1043116ec4ec8ce0d15ba80b…" ]

        const splitted: string[] = currSrc.split("/");
        splitted[4] = splitted[4].replace("th_", "");
        return "http://" + splitted[2] + "/img.php?image=" + splitted[4];
    }

    private static sexyimgurlHandler(currSrc: string): string {
        return currSrc.replace("small", "big");
    }

    // var sexyimgurlhandler = function(currSrc) {
    // 	return currSrc.replace("small", "big");
    // }

    private static imagezillaHandler(currSrc: string): string {
        //  final url:			http://imagezilla.net/images/bboyTIt-33c_0001.jpg

        //  thumbnail img src:	http://imagezilla.net/thumbs2/bboyTIt-33c_0001_tn.jpg
        //  image link href:	http://imagezilla.net/show/bboyTIt-33c_0001.jpg

        return currSrc.replace("_tn", "").replace("/thumbs2", "/images");
    }

    private static imgdinoHandler(currSrc: string): string {
        // http://img2.imgdino.com/images/40484973034650128355_thumb.jpg
        return currSrc.replace("_thumb", "");
    }

    private static imgchiliHandler(currSrc: string): string {
        // http://imgchili.net/show/84965/84965322_ro9_0003.jpg
        // http://i8.imgchili.net/84965/84965322_ro9_0003.jpg
        const splitted: string[] = currSrc.split("/");

        return "http://" + splitted[2].replace("t8", "i8") + "/" + splitted[3] + "/" + splitted[4];
    }

    private static pixhostHandler(currSrc: string): string {
        // https://t19.pixhost.to/thumbs/255/70943962_img_0002.jpg
        // https://img19.pixhost.to/images/255/70943962_img_0002.jpg

        // https://t19.pixhost.to/thumbs/255/70943960_img_0001.jpg
        // https://img19.pixhost.to/images/255/70943960_img_0001.jpg

        const splitted: string[] = currSrc.split("/");

        return "https://" + splitted[2].replace("t", "img") + "/images/" + splitted[4] + "/" + splitted[5];
    }

    private static fappicHandler(currSrc: string): string {
        // http://fappic.com/small/KZnf.jpg
        const splitted: string[] = currSrc.split("/");
        return "http://fappic.com/" + splitted[4];
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

    private static  imagetwistHandler(currSrc: string): string {

        // final url:           https://img65.imagetwist.com/i/19425/qbkfz2r7aro8.JPG/IMG_0001.JPG
        // final url 2:			https://img161.imagetwist.com/i/20897/yvsryjdup9wb.jpg/ltZ_0002.jpg
        // 						https://img161.imagetwist.com/th/20897/yvsryjdup9wb.jpg
        // thumbnail img src:   https://img65.imagetwist.com/th/19425/qbkfz2r7aro8.jpg
        // image link href:     https://beautifulteenmodels.urlgalleries.net/porn-picture-76r17thodv.jpg

        return currSrc.replace("/th/", "/i/").replace("jpg", "jpeg");
    }

    private static acidimgHandler(currSrc: string): string {

        // https://acidimg.cc/upload/small/2018/01/15/5a5ce095075b2.JPG
        // https://acidimg.cc/upload/small/2017/07/13/5967ba55bd779.JPG
        // https://i.acidimg.cc/big/2017/07/13/5967ba55bd779.JPG

        return currSrc.replace("small", "big");
    }

    private static imagebamHandler(currSrc: string): string {
        // orig src: http://thumbs2.imagebam.com/e8/03/83/d6c3b4959882404.jpg
        //orig link: http://www.imagebam.com/image/d6c3b4959882404
        // dest src: http://images2.imagebam.com/2f/48/5c/d6c3b4959882404.jpg

        return "NO-IMPLEMENTATION";
    }

    private static imgboxHandler(currSrc: string): string {
        // input: 	https://thumbs2.imgbox.com/bf/b6/5QO4uGfw_t.jpg
        // output: 	https://images2.imgbox.com/bf/b6/5QO4uGfw_o.jpg	

        return currSrc.replace("thumbs", "images").replace("_t", "_o");
    }

    private static imxHandler(currSrc: string): string {

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

    private static getFtpName(source: string): FtpSite {

        if (source.includes("imagevenue"))
            return FtpSite.imagevenue;

        if (source.includes("sexyimg"))
            return FtpSite.sexyimg;

        if (source.includes("imagezilla"))
            return FtpSite.imagezilla;

        if (source.includes("imagetwist"))
            return FtpSite.imagetwist;

        if (source.includes("imgdino"))
            return FtpSite.imgdino;

        if (source.includes("img.yt"))
            return FtpSite["img.yt"];

        if (source.includes("imgchili"))
            return FtpSite.imgchili;

        if (source.includes("pixhost"))
            return FtpSite.pixhost;

        if (source.includes("fappic"))
            return FtpSite.fappic;

        if (source.includes("acidimg"))
            return FtpSite.acidimg;

        if (source.includes("imagebam"))
            return FtpSite["NO-IMPLEMENTATION"];

        if (source.includes("imgbox"))
            return FtpSite.imgbox;

        if (source.includes("imx"))
            return FtpSite.imx;

        if (source.includes("imgserve"))
            return FtpSite.imgserve;

        return FtpSite["NO-IMPLEMENTATION"];
    }
}
