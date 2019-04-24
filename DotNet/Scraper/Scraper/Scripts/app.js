var Global = {
    scraper: "/home/get",
    galleryScraper: "/home/gallery",
    actualUrl: "",
    base: "https://urlgalleries.net",
    searchTerm: "",
    currentPage: 1,
    domFilter: {},
    sidePager: {}
}

function Request() {

    var searchTerm = document.getElementById("requestUrl").value;
    if (searchTerm === "") {
        Global.actualUrl = "/home/GetHome"
            + "?url=" + Global.base
            + "&p=" + Global.currentPage;

        var httpRequest = Http.Get(Global.actualUrl);
        httpRequest.then(HandleHtmlResponse);
    }
    else {
        Global.searchTerm = searchTerm;
        Global.actualUrl = Global.scraper
            + "/?p=" + Global.currentPage
            + "&t=10"
            + "&q=" + Global.searchTerm;

        var httpRequest = Http.Get(Global.actualUrl);
        httpRequest.then(HandleUrlGalleriesPage);
    }

    Global.currentPage = ++Global.currentPage;
}

function HandleHtmlResponse(response) {

    var parser = new DOMParser();
    var doc = parser.parseFromString(response, "text/html");

    var contents = doc.body.childNodes;
    for (var i = 0; i < contents.length; ++i) {
        document.body.append(contents[i]);
    } 
}

function HandleUrlGalleriesPage(responseJson) {

    var galleryThumbs = JSON.parse(responseJson);
    var contentDiv = document.getElementById("content");

    for (var i = 0; i < galleryThumbs.length; ++i) {
        contentDiv.append(ConvertGalleryThumb(galleryThumbs[i]));
    }
}

function ConvertGalleryThumb(galleryThumbData) {
    var galleryThumb = document.createElement("div");

    var title = document.createElement("h3");
    title.innerText = galleryThumbData.Title;
    title.setAttribute("data-link", galleryThumbData.Link);
    title.setAttribute("onclick", "OpenPage(this)");
    galleryThumb.append(title);

    for (var i = 0; i < 3; ++i) {
        var newImg = document.createElement("img");
        newImg.setAttribute("src", galleryThumbData.ImageLinks[i]);
        galleryThumb.append(newImg);
    }

    galleryThumb.classList.add("gallery-thumb");
    return galleryThumb;
}

function OpenPage(element) {
    var href = "https:" + element.getAttribute("data-link");

    var httpRequest = Http.Get(Global.galleryScraper + "?url=" + href);

    httpRequest.then(function (response) {
        var imageSrcs = JSON.parse(response);

        var sidePage = document.createElement("div");
        // var contents = doc.querySelectorAll('a img');
        for (var i = 0; i < imageSrcs.length; ++i) {
            var newImg = document.createElement("img");
            newImg.setAttribute("src", imageSrcs[i]);
            sidePage.append(newImg);
        }

        Global.sidePager.CreatePage(sidePage);
    });
}

function Clear() {

    document.getElementById("content").innerHTML = "";
}

function FilterClick() {
    Global.domFilter.ResetFilter();
    var filterPredicateValue = document.getElementById("filter-predicate-value").value;
    if (filterPredicateValue !== "")
        Global.domFilter.Filter((element) => {
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
    Global.domFilter.ResetFilter();
}

(function () {
    Global.domFilter = new Nadam.DomFilter("#content .gallery-thumb h3", "#content .gallery-thumb");
    Global.sidePager = new Nadam.SidePager();
})();