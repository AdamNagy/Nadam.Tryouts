var AppControls = {

    domFilter: {},
    sidePager: {},
    components: new Array()
}


var AppConfig = {

    scraper: "/home/get",
    galleryScraper: "/home/gallery",
    base: "https://urlgalleries.net",
}

var AppState = {

    actualUrl: "",
    searchTerm: "",
    currentPage: 0,

}

function Request() {

    AppState.currentPage = document.getElementById("page").value;
    var searchTerm = document.getElementById("requestUrl").value;
    if (searchTerm === "") {
        AppState.actualUrl = "/home/GetHome"
            + "?url=" + AppConfig.base
            + "&p=" + AppState.currentPage;

        var httpRequest = Nadam.Http.Get(AppState.actualUrl);
        httpRequest.then(HandleHtmlResponse);
    }
    else {
        AppState.searchTerm = searchTerm;
        AppState.actualUrl = AppConfig.scraper
            + "/?p=" + AppState.currentPage
            + "&t=10"
            + "&q=" + AppState.searchTerm;

        var httpRequest = Nadam.Http.Get(AppState.actualUrl);
        httpRequest.then(HandleUrlGalleriesPage);
    }

    // AppState.currentPage = ++AppState.currentPage;
    document.getElementById("page").value = Number(AppState.currentPage) + 1;
}

function RequestP10() {

    var searchTerm = document.getElementById("requestUrl").value;
    AppState.currentPage = document.getElementById("page").value;
    AppState.searchTerm = searchTerm;
    var lastPage = Number(document.getElementById("page").value) + 10;
    var requestUrls = new Array();

    while (AppState.currentPage < lastPage) {

        AppState.actualUrl = AppConfig.scraper
            + "/?p=" + AppState.currentPage
            + "&t=10"
            + "&q=" + AppState.searchTerm;

        requestUrls.push(AppState.actualUrl);

        AppState.currentPage++;
    }

    Nadam.Http.QueuedGet(requestUrls, 3, HandleUrlGalleriesPage);
    document.getElementById("page").value = Number(lastPage) + 1;
}


function HandleHtmlResponse(response) {

    var parser = new DOMParser();
    var doc = parser.parseFromString(response, "text/html");
    var contentDiv = document.getElementById("content");

    var contents = doc.body.childNodes;
    for (var i = 0; i < contents.length; ++i) {
        contentDiv.append(contents[i]);
    } 
}

var pages = 1;
function HandleUrlGalleriesPage(responseJson) {

    var galleryThumbnails = JSON.parse(responseJson);
    var contentDiv = document.getElementById("content");

    var separator = document.createElement("div");
    separator.classList.add("separator");
    var pageNum = document.createElement("h1");
    pageNum.innerText = "Page: " + Number(pages++);

    separator.append(pageNum);
    contentDiv.append(separator);
    for (var i = 0; i < galleryThumbnails.length; ++i) {
        contentDiv.append(ConvertGalleryThumb(galleryThumbnails[i]));
    }
}

function ConvertGalleryThumb(galleryThumbnail) {
    var galleryThumb_element = document.createElement("div");

    var title_element = document.createElement("h3");
    title_element.innerText = galleryThumbnail.Title;
    title_element.setAttribute("data-link", galleryThumbnail.SourceUrl);
    title_element.setAttribute("onclick", "OpenPage(this)");
    galleryThumb_element.append(title_element);

    var sampleImageContainer_element = document.createElement("div");
    sampleImageContainer_element.classList.add("d-flex");
    sampleImageContainer_element.classList.add("justify-content-center");

    for (var i = 0; i < 3; ++i) {
        var imgContainer_element = document.createElement("div");
        imgContainer_element.classList.add("imgContainer_element");

        var newImg = document.createElement("img");
        newImg.setAttribute("src", galleryThumbnail.ThumbnailImageSources[i]);
        imgContainer_element.append(newImg);
        sampleImageContainer_element.append(imgContainer_element);
    }

    galleryThumb_element.append(sampleImageContainer_element);
    galleryThumb_element.classList.add("gallery-thumb");
    return galleryThumb_element;
};

function OpenPage(element) {
    galleryUrl = element.getAttribute("data-link");
    galleryTitle = element.getAttribute("data-title");
    var href = "https:" + galleryUrl;

    var httpRequest = Nadam.Http.Get(AppConfig.galleryScraper + "?url=" + href);

    httpRequest.then(function (response) {
        var imageMetas = JSON.parse(response);

        var galleryModel = new Nadam.Webhack.GalleryModel();
        galleryModel.SourceUrl = galleryUrl;
        galleryModel.ImagesMetaData = imageMetas;
        galleryModel.Title = galleryTitle;
        var galleryComponent = new Nadam.Webhack.GalleryComponent(galleryModel);

        AppControls.sidePager.CreatePage(galleryComponent.item);
        AppControls.components.push(galleryComponent);
    });
};

function Clear() {

    document.getElementById("content").innerHTML = "";
}

function FilterClick() {
    AppControls.domFilter.ResetFilter();
    var filterPredicateValue = document.getElementById("filter-predicate-value").value;
    if (filterPredicateValue !== "")
        AppControls.domFilter.Filter((element) => {
            if (element) {
                filterPredicateValue = filterPredicateValue.replace(' ', '').toLowerCase();
                var actual = element.innerText.replace(' ', '').toLowerCase();
                if (filterPredicateValue.lastIndexOf('*') > 0)
                    return actual.startsWith(filterPredicateValue.substr(0, filterPredicateValue.length - 1));
                else
                    return actual == filterPredicateValue;
            }
            return false;
        });
}

function ResetFilterClick() {
    document.getElementById("filter-predicate-value").value = "";
    AppControls.domFilter.ResetFilter();
}

(function () {
    AppControls.domFilter = new Nadam.DomFilter("#content .gallery-thumb h3", "#content .gallery-thumb");
    AppControls.sidePager = new Nadam.SidePager();

    document.getElementById("page").value = 1;
})();