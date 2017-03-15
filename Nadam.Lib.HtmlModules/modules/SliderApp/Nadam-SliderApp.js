"use strict";
function SliderManager(pages) {
    // private:
    var version = "v1.0.0";
    var pageInitHandler = new PageInitializer(pages);

    // public:
    this.currPage = 0;
    this.pages = pages;

    this.start = function () {
        this.slide(1);
    }

    this.slide = function (page) {
        if (page > this.pages) {
            page = 1;
        }
        else if (page < 1) {
            page = this.pages;
        }

        try {
            pageInitHandler['page' + page]();
        }
        catch (err) {
            console.log(err);
        }

        $('#slide-' + this.currPage).hide();
        $('#manu-button-' + this.currPage).removeClass("menu-button-current");

        this.currPage = page;
        $('#slide-' + this.currPage).show('fade', 500);
        $('#manu-button-' + this.currPage).addClass('menu-button-current');
    }
}

function PageInitializer(pageNum) {
    var pagesLoadStatus = {};
    var length = pageNum;

    this.page1 = function () {
    }

    this.page2 = function () {
        if (pagesLoadStatus[2] == false) {
            RefreshRandoms("black");
            pagesLoadStatus[2] = true;
        }
    }

    this.page3 = function () {
        if (pagesLoadStatus[3] == false) {
            GetLatesSequences();
            pagesLoadStatus[3] = true;
        }
    }

    // constructor
    var constructor = function () {
        for (var i = 1; i <= length; ++i) {
            pagesLoadStatus[i] = false;
        }
    }();
}

function slideToNextPage(nextPage) {
    if (nextPage == null)
        nextPage = app.currPage + 1;

    app.slide(nextPage);
}

function slideToPrevPage(nextPage) {
    if (nextPage == null)
        nextPage = app.currPage - 1;

    app.slide(nextPage);
}