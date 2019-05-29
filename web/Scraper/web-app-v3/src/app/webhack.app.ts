import { DomFilter } from "../nadam/nadam.dom-filter.control";
import { Http } from "../nadam/nadam.http.lib";
import { SidePager } from "../nadam/nadam.side-pager.control";
import { GalleryModel, GalleryThumbnailModel, ImageMetadataModel } from "./webhack.galley.model";
import { GalleryComponent } from "./webhack.gallery.component";

import "./webhack.app.scss";

export class Dom {

	public static GetElementById(id: string): HTMLElement {
		return (document.getElementById(id) as HTMLElement);
	}

	public static GetInputElementById(id: string): HTMLInputElement {
		return this.GetElementById(id) as HTMLInputElement;
	}
}

export class App {

	View: HTMLElement;
	Pages: number = 1;

	Controls: any = {

		DomFilter: DomFilter,
		SidePager: SidePager,
		Components: new Array(),
		Http: new Http()
	};

	Config: any = {

		Scraper: "http://localhost:36344/home/get",
		GalleryScraper: "http://localhost:36344/home/gallery",
		Base: "https://urlgalleries.net"
	};

	State: any = {

		ActualUrl: "",
		SearchTerm: "",
		CurrentPage: 0

	};

	constructor() {

		this.View = document.createElement("div");
		this.View.classList.add("content");

		this.Controls.domFilter = new DomFilter("#content .gallery-thumb h3", "#content .gallery-thumb");
		Dom.GetElementById("control-panel-slot-2")!.append(this.Controls.domFilter.view);
        this.Controls.sidePager = new SidePager();
    }

    public Request(): void {

        this.State.currentPage = (document.getElementById("page-input") as HTMLInputElement).value;
        let searchTerm: string = (document.getElementById("requestUrl") as HTMLInputElement).value;
        if (searchTerm === "") {
            this.State.actualUrl = "/home/GetHome"
                + "?url=" + this.Config.base
                + "&p=" + this.State.currentPage;

            let httpRequest: Promise<any> = Http.Get(this.State.actualUrl);
            httpRequest.then(this.HandleHtmlResponse);
        } else {
            this.State.searchTerm = searchTerm;
            this.State.actualUrl = this.Config.Scraper
                + "/?p=" + this.State.currentPage
                + "&t=10"
                + "&q=" + this.State.searchTerm;

            let httpRequest: Promise<any> = Http.Get(this.State.actualUrl);
            httpRequest.then((result) => { this.HandleUrlGalleriesPage(result); });
        }

        (document.getElementById("page-input") as HTMLInputElement).value = (Number(this.State.currentPage) + 1).toString();
    }

    public HandleUrlGalleriesPage(responseJson: string): void {

        let galleryThumbnails: Array<GalleryThumbnailModel> = JSON.parse(responseJson);
        let contentDiv: HTMLElement = Dom.GetElementById("content");

        let separator: HTMLElement = document.createElement("div");
        separator.classList.add("separator");
        let pageNum: HTMLElement = document.createElement("h1");
        pageNum.innerText = "Page: " + Number(this.Pages++);

        separator.append(pageNum);
        contentDiv.append(separator);
        for (let i: number = 0; i < galleryThumbnails.length; ++i) {
            contentDiv.append(this.ConvertGalleryThumb(galleryThumbnails[i]));
        }
    }

    ConvertGalleryThumb(galleryThumbnail: GalleryThumbnailModel): HTMLElement {

        let galleryThumb_element: HTMLElement = document.createElement("div");

        let title_element: HTMLElement = document.createElement("h3");
        title_element.innerText = galleryThumbnail.Title;
        title_element.setAttribute("data-link", galleryThumbnail.SourceUrl);

        title_element.addEventListener("click", ((_galleryThumbnail) => {
            return () => { this.OpenPage(_galleryThumbnail); };
        })(galleryThumbnail));

        galleryThumb_element.append(title_element);

        let sampleImageContainer_element: HTMLElement = document.createElement("div");
        sampleImageContainer_element.classList.add("d-flex");
        sampleImageContainer_element.classList.add("justify-content-center");

        for (let i: number = 0; i < 3; ++i) {
            let imgContainer_element: HTMLElement = document.createElement("div");
            imgContainer_element.classList.add("imgContainer_element");

            let newImg: HTMLElement = document.createElement("img");
            newImg.setAttribute("src", galleryThumbnail.ThumbnailImageSources[i]);
            imgContainer_element.append(newImg);
            sampleImageContainer_element.append(imgContainer_element);
        }

        galleryThumb_element.append(sampleImageContainer_element);
        galleryThumb_element.classList.add("gallery-thumb");
        return galleryThumb_element;
    }

    RequestP10(): void {

        let searchTerm: string = (document.getElementById("requestUrl") as HTMLInputElement).value;
        this.State.currentPage = (document.getElementById("page-input") as HTMLInputElement).value;
        this.State.searchTerm = searchTerm;
        let lastPage: number = Number(Dom.GetInputElementById("page-input").value) + 10;
        let requestUrls: Array<string> = new Array();

        while (this.State.currentPage < lastPage) {

            this.State.actualUrl = this.Config.Scraper
                + "/?p=" + this.State.currentPage
                + "&t=10"
                + "&q=" + this.State.searchTerm;

            requestUrls.push(this.State.actualUrl);

            this.State.currentPage++;
        }

        this.Controls.http.QueuedGet(requestUrls, 3, (response: string) => { return this.HandleUrlGalleriesPage(response); });
        Dom.GetInputElementById("page-input").value = (Number(lastPage) + 1).toString();
    }

    HandleHtmlResponse(response: string): void {

        let parser: DOMParser = new DOMParser();
        let doc: Document = parser.parseFromString(response, "text/html");
        let contentDiv: HTMLElement = Dom.GetElementById("content");

        let contents: NodeListOf<ChildNode> = doc.body.childNodes;
        for (let i: number = 0; i < contents.length; ++i) {
            contentDiv.append(contents[i]);
        }
    }

    OpenPage(galleryModel: GalleryThumbnailModel): void {
        let galleryUrl: string = galleryModel.SourceUrl;
        let galleryTitle: string = galleryModel.Title;
        let href: string = "https:" + galleryUrl;

        let httpRequest: Promise<string> = Http.Get(this.Config.GalleryScraper + "?url=" + href);

        httpRequest.then((response) => { return this.HandleNewGalleryCreation(response, galleryModel); });
    }

    HandleNewGalleryCreation(response: string, galleryResponseModel: GalleryThumbnailModel): void {

        var imageMetas: Array<ImageMetadataModel> = JSON.parse(response);

        let galleryModel: GalleryModel = new GalleryModel();
        galleryModel.SourceUrl = galleryResponseModel.SourceUrl;
        galleryModel.ImagesMetaData = imageMetas;
        galleryModel.Title = galleryResponseModel.Title;
        let galleryComponent: GalleryComponent = new GalleryComponent(galleryModel);

        this.Controls.sidePager.CreatePage(galleryComponent.View);
        this.Controls.components.push(galleryComponent);
    }

    Clear() {

        this.View.innerHTML = "";
    }
}