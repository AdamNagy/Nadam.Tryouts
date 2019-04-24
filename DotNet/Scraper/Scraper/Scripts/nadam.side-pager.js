"use strict";

window.Nadam = window.Nadam || {}

Nadam.SidePager = function () {

    var self = this;
    var pageManager;
    var pages = new Array();
    var openPages = 0;

    var createPage = function () {

        var pageElement = document.createElement('div');

        pageElement.classList.add("side-page");
        pageElement.style.zIndex = 10 + pages.length;

        var opener = document.createElement("div");
        opener.style.height = "100%";
        opener.style.width = "30px"
        opener.style.float = "left";
        opener.addEventListener("click", (function (idx) {
            return function () {
                openPage(idx);
            };
        })(pages.length));

        var closeCurrent = document.createElement("button");
        closeCurrent.addEventListener("click", (function (idx) {
            return function () {
                closePage(idx);
            }
        })(pages.length));
        closeCurrent.innerText = "Close";

        pageElement.append(closeCurrent);
        pageElement.append(opener);

        pages.push(pageElement);
        pageManager.append(pageElement);
        self.CloseAll();
    };

    var openPage = function (pageIdx) {

        var firstPageLeft = pageIdx > 0 ? (pageIdx)*50 : 0;
        for (var i = 0; i <= pageIdx; ++i) {
            pages[i].style.right = firstPageLeft + "px";
            pages[i].classList.add("side-page-open");
            pages[i].classList.remove("side-page-closed");
            firstPageLeft -= 50;
        }
        document.body.style.overflowY = "hidden";
        openPages++;
    };

    var closePage = function (pageIdx) {

        var firstPageLeft = pages.length * 50;
        for (var i = pageIdx; i < pages.length; ++i) {
            pages[i].style.right = "-" + (720 - firstPageLeft) + "px";
            pages[i].classList.remove("side-page-open");
            pages[i].classList.add("side-page-closed");
            firstPageLeft = firstPageLeft - 50;
        }
        openPages--;
        if (openPages <= 0)
            document.body.style.overflowY = "auto";
    }

    this.CloseAll = function () {

        var firstPageLeft = pages.length * 50;
        for (var i = 0; i < pages.length; ++i) {
            pages[i].style.right = "-" + (720 - firstPageLeft) + "px";
            pages[i].classList.remove("side-page-open");
            pages[i].classList.add("side-page-closed");
            firstPageLeft = firstPageLeft - 50;
        }

        document.body.style.overflowY = "auto";
    };

    this.CreatePage = function (rootElement) {

        createPage();
        var addedPage = pages[pages.length - 1];
        addedPage.append(rootElement);
    };

    (function () {

        pageManager = document.createElement('div');
        pageManager.style.position = 'fixed';
        pageManager.id = "side-pages-manager";
        document.body.append(pageManager);
    })();
}