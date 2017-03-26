"use strict";
function SliderManager() {
    // private:
    var currPage = 0;
    var sumPages = 0;
    var pagesLoadStatus = {};           // bool type array to indicate if the slide has been visited or nor
    var pageSelectionListeners = {};    // page load event listeners array, contains function pointer that will be invoked at slide load

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
    /// <summary>
    /// Event that is fired at every slide first load only once
    /// can be used to initialize the slide befire visit
    /// </summary>
    this.SlideLoadListener = function (pagenum, func) {
        pageSelectionListeners[pagenum] = func;
    }

    /// <summary>
    /// slides to the given page
    /// </summary>
    /// <param name="tpPage">the page number</param>
    this.ToPage = function (tpPage) {
        slide(tpPage);
    }

    /// <summary>
    /// slides to next page
    /// </summary>
    this.NextPage = function() {
        slide(currPage+1);
    }

    /// <summary>
    /// slides to previous page
    /// </summary>
    this.PrevPage = function () {
        slide(currPage - 1);
    }

    /// <summary>
    /// start the slider with the given page
    /// </summary>
    this.start = function () {
        slide(1);
    }

    /// <summary>
    /// Constructor
    /// </summary>
    var ctor = function () {
        var numOfPages = $('.slide').length;
        sumPages = numOfPages;
        for (var i = 1; i <= sumPages; ++i) {
            pagesLoadStatus[i] = false;
        }
    }();
}