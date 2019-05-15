

export class GalleryModel {

    constructor() {

        this.SourceUrl;
        this.Title;
        this.ImagesMetaData = new Array();
        this.FtpSite = "";  
    }
    
    get Id () {
		return id;
    }

   
    get Domain() {

        if (doain == undefined) {
            var splitted = self.SourceUrl.split('/');
            domain = splitted[2];
        }

        return domain;
    }
}