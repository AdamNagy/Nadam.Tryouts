"use strict";
function SliderManager() {
    // private:
    var currPage = 0;
    var sumPages = 0;
    var pagesLoadStatus = {};
    var pageSelectionListeners = {};

    var slide = function (page) {
        if (page > sumPages) {
            page = 1;
        }
        else if (page < 1) {
            page = sumPages;
        }

        try {
            if (pagesLoadStatus[page] == false) {
                pageSelectionListeners[page]();
                pagesLoadStatus[page] = true;
            }                
        }
        catch (err) {
            console.log("page " + page + " does not have any initializer function");
        }

        $('#slide-' + currPage).hide();
        $('#manu-button-' + currPage).removeClass("menu-button-current");

        currPage = page;
        $('#slide-' + currPage).show('fade', 500);
        $('#manu-button-' + currPage).addClass('menu-button-current');
    }

    // public:
    this.SlideLoadListener = function (pagenum, func) {
        pageSelectionListeners[pagenum] = func;
    }

    this.ToPage = function (tpPage) {
        slide(tpPage);
    }

    this.NextPage = function() {
        slide(currPage+1);
    }

    this.PrevPage = function () {
        slide(currPage - 1);
    }

    this.start = function () {
        slide(1);
    }

    // Constructor
    var ctor = function () {
        var numOfPages = $('.slide').length;
        sumPages = numOfPages;
        for (var i = 1; i <= sumPages; ++i) {
            pagesLoadStatus[i] = false;
        }
    }();
}