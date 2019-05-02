var AppControls = {

    domFilter: {},
    sidePager: {}
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

    var galleryThumbs = JSON.parse(responseJson);
    var contentDiv = document.getElementById("content");

    var separator = document.createElement("div");
    separator.classList.add("separator");
    var pageNum = document.createElement("h1");
    pageNum.innerText = "Page: " + Number(pages++);

    separator.append(pageNum);
    contentDiv.append(separator);
    for (var i = 0; i < galleryThumbs.length; ++i) {
        contentDiv.append(ConvertGalleryThumb(galleryThumbs[i]));
    }
}

function ConvertGalleryThumb(galleryThumbData) {
    var galleryThumb = document.createElement("div");
    var flexContainer = document.createElement("div");

    var title = document.createElement("h3");
    title.innerText = galleryThumbData.Title;
    title.setAttribute("data-link", galleryThumbData.Link);
    title.setAttribute("onclick", "OpenPage(this)");
    galleryThumb.append(title);

    for (var i = 0; i < 3; ++i) {
        var newImg = document.createElement("img");
        newImg.setAttribute("src", galleryThumbData.ImageLinks[i]);
        flexContainer.append(newImg);
    }

    galleryThumb.classList.add("gallery-thumb");


    galleryThumb.append(flexContainer);
    return galleryThumb;
}

function OpenPage(element) {
    var href = "https:" + element.getAttribute("data-link");

    var httpRequest = Nadam.Http.Get(AppConfig.galleryScraper + "?url=" + href);

    httpRequest.then(function (response) {
        var imageSrcs = JSON.parse(response);

        var sidePage = document.createElement("div");
        for (var i = 0; i < imageSrcs.length; ++i) {
            var newImg = document.createElement("img");
            newImg.setAttribute("src", imageSrcs[i].SampleImageSrc);
            sidePage.append(newImg);
        }

        AppControls.sidePager.CreatePage(sidePage);
    });
}

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