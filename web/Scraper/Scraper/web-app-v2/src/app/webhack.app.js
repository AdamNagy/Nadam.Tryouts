import { DomFilter } from "../nadam/nadam.dom-filter.control";
import { Http } from "../nadam/nadam.http.lib";
import { SidePager } from "../nadam/nadam.side-pager.control";
import { GalleryModel } from "./webhack.galley.model";
import { GalleryComponent } from "./webhack.gallery.component";

export class App {

    constructor() {

        this.pages = 1;
        this.Controls = {

            domFilter: {},
            sidePager: {},
            components: new Array(),
            http: new Http()
        };

        this.Config = {

            scraper: "/home/get",
            galleryScraper: "/home/gallery",
            base: "https://urlgalleries.net"
        };

        this.State = {

            actualUrl: "",
            searchTerm: "",
            currentPage: 0

        };

        this.Controls.domFilter = new DomFilter("#content .gallery-thumb h3", "#content .gallery-thumb");
        this.Controls.sidePager = new SidePager();
    }

    Request() {

        this.State.currentPage = document.getElementById("page-input").value;
        let searchTerm = document.getElementById("requestUrl").value;
        if (searchTerm === "") {
            this.State.actualUrl = "/home/GetHome"
                + "?url=" + this.Config.base
                + "&p=" + this.State.currentPage;

            let httpRequest = Http.Get(this.State.actualUrl);
            httpRequest.then(this.HandleHtmlResponse);
        }
        else {
            this.State.searchTerm = searchTerm;
            this.State.actualUrl = this.Config.scraper
                + "/?p=" + this.State.currentPage
                + "&t=10"
                + "&q=" + this.State.searchTerm;

            let httpRequest = Http.Get(this.State.actualUrl);
            httpRequest.then((result) => { this.HandleUrlGalleriesPage(result); });
        }

        document.getElementById("page-input").value = Number(this.State.currentPage) + 1;
    }

    HandleUrlGalleriesPage(responseJson) {

        let galleryThumbnails = JSON.parse(responseJson);
        let contentDiv = document.getElementById("content");

        let separator = document.createElement("div");
        separator.classList.add("separator");
        let pageNum = document.createElement("h1");
        pageNum.innerText = "Page: " + Number(this.pages++);

        separator.append(pageNum);
        contentDiv.append(separator);
        for (let i = 0; i < galleryThumbnails.length; ++i) {
            contentDiv.append(this.ConvertGalleryThumb(galleryThumbnails[i]));
        }
    }

    ConvertGalleryThumb(galleryThumbnail) {

        let galleryThumb_element = document.createElement("div");

        let title_element = document.createElement("h3");
        title_element.innerText = galleryThumbnail.Title;
        title_element.setAttribute("data-link", galleryThumbnail.SourceUrl);

        title_element.addEventListener("click", ((_galleryThumbnail) => {
            return () => { this.OpenPage(_galleryThumbnail); };
        })(galleryThumbnail));

        galleryThumb_element.append(title_element);

        let sampleImageContainer_element = document.createElement("div");
        sampleImageContainer_element.classList.add("d-flex");
        sampleImageContainer_element.classList.add("justify-content-center");

        for (let i = 0; i < 3; ++i) {
            let imgContainer_element = document.createElement("div");
            imgContainer_element.classList.add("imgContainer_element");

            let newImg = document.createElement("img");
            newImg.setAttribute("src", galleryThumbnail.ThumbnailImageSources[i]);
            imgContainer_element.append(newImg);
            sampleImageContainer_element.append(imgContainer_element);
        }

        galleryThumb_element.append(sampleImageContainer_element);
        galleryThumb_element.classList.add("gallery-thumb");
        return galleryThumb_element;
    }

    RequestP10() {

        let searchTerm = document.getElementById("requestUrl").value;
        this.State.currentPage = document.getElementById("page-input").value;
        this.State.searchTerm = searchTerm;
        let lastPage = Number(document.getElementById("page-input").value) + 10;
        let requestUrls = new Array();

        while (this.State.currentPage < lastPage) {

            this.State.actualUrl = this.Config.scraper
                + "/?p=" + this.State.currentPage
                + "&t=10"
                + "&q=" + this.State.searchTerm;

            requestUrls.push(this.State.actualUrl);

            this.State.currentPage++;
        }

        this.Controls.http.QueuedGet(requestUrls, 3, (response) => { return this.HandleUrlGalleriesPage(response); });
        document.getElementById("page-input").value = Number(lastPage) + 1;
    }

    HandleHtmlResponse(response) {

        let parser = new DOMParser();
        let doc = parser.parseFromString(response, "text/html");
        let contentDiv = document.getElementById("content");

        let contents = doc.body.childNodes;
        for (let i = 0; i < contents.length; ++i) {
            contentDiv.append(contents[i]);
        }
    }

    OpenPage(galleryModel) {
        let galleryUrl = galleryModel.SourceUrl;
        let galleryTitle = galleryModel.Title;
        let href = "https:" + galleryUrl;

        let httpRequest = Http.Get(this.Config.galleryScraper + "?url=" + href);

        httpRequest.then((response) => { return this.HandleNewGalleryCreation(response, galleryModel); });
    }

    HandleNewGalleryCreation(response, galleryResponseModel) {

        var imageMetas = JSON.parse(response);

        let galleryModel = new GalleryModel();
        galleryModel.SourceUrl = galleryResponseModel.SourceUrl;
        galleryModel.ImagesMetaData = imageMetas;
        galleryModel.Title = galleryResponseModel.Title;
        let galleryComponent = new GalleryComponent(galleryModel);

        this.Controls.sidePager.CreatePage(galleryComponent.item);
        this.Controls.components.push(galleryComponent);
    }

    Clear() {

        document.getElementById("content").innerHTML = "";
    }

    FilterClick() {
        AppControls.domFilter.ResetFilter();
        let filterPredicateValue = document.getElementById("filter-predicate-value").value;
        if (filterPredicateValue !== "")
            AppControls.domFilter.Filter((element) => {
                if (element) {
                    filterPredicateValue = filterPredicateValue.replace(' ', '').toLowerCase();
                    let actual = element.innerText.replace(' ', '').toLowerCase();
                    if (filterPredicateValue.lastIndexOf('*') > 0)
                        return actual.startsWith(filterPredicateValue.substr(0, filterPredicateValue.length - 1));
                    else
                        return actual === filterPredicateValue;
                }
                return false;
            });
    }

    ResetFilterClick() {
        document.getElementById("filter-predicate-value").value = "";
        AppControls.domFilter.ResetFilter();
    }
}