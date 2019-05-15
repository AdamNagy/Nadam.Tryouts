// v.1.0.1

"use strict";

/// usage:
/// var pageManager = new SidePager();
///	pageManager.CreatePage(document.createElement("div").innerText = "Hello world");
window.Nadam = window.Nadam || {}; 
Nadam.SidePager = function() {

	var self = this;
	var pageManager;
	var pages = new Array();
	var domParser = new DOMParser();
	var openPages = 0;

	var pageWidth = 720;
	var spaceBetweenPages = 50;

	var template = 
		`<div class="side-page">
			<h3>#title#</h3> 
			<button data-local-id='close-btn'>Close</button>
            <button data-local-id='remove-btn'>Remove</button>
			<div class="side-page-opener"></div>
			<div class="side-page-content"></div>
		</div>`;

	var createPage = function(title) {

		if( title === undefined )
			title = "";
			
		var instanceTemplate = template.replace("#title#", title);
		var pageElement = domParser.parseFromString(instanceTemplate, "text/html")
									.querySelector("div:first-child");
		pageElement.style.zIndex = 11 + pages.length;		

		pageElement.querySelector("div[class='side-page-opener']")
			.addEventListener("click", (function (idx) {
				return function () {
					openPage(idx);
				};
			})(pages.length));

        pageElement.querySelector("button[data-local-id='close-btn']")
			.addEventListener("click", (function (idx) {
				return function () {
					closePage(idx);
				};
            })(pages.length));

        pageElement.querySelector("button[data-local-id='remove-btn']")
            .addEventListener("click", (function (idx) {
                return function () {
                    self.RemovePage(idx);
                };
            })(pages.length));

        pages.push({ id: pages.length, element: pageElement });
		pageManager.append(pageElement);
		self.CloseAll();
	};

	var openPage = function (pageId) {

		var index = getIndex(pageId);
		var closedPagesWidth = (pages.length - 1 - index) * spaceBetweenPages;
		var openPages_buffer = index * spaceBetweenPages;

        var pageRightPosition = closedPagesWidth + openPages_buffer;
        for (var i = 0; i <= index; ++i) {

            var currentElement = pages[i];
            currentElement.element.style.right = pageRightPosition + "px";
            currentElement.element.classList.add("side-page-open");
            currentElement.element.classList.remove("side-page-closed");
            pageRightPosition -= 50;
        }

        document.body.style.overflowY = "hidden";
        openPages++;
    };

    var closePage = function (pageId) {

		var index = getIndex(pageId);

		var buffer = pages.length - index - 1; 
        var pageRightPosition = (buffer * 50) + 50;
        for (var i = index; i < pages.length; ++i) {
            var actual = pages[i];
            actual.element.style.left = null;
            actual.element.style.width = pageWidth + "px";
			actual.element.style.right = "-" + (720 - pageRightPosition) + "px";
			
            actual.element.classList.remove("side-page-open");
            actual.element.classList.add("side-page-closed");
            pageRightPosition -= 50;
        }

        openPages--;

        if (openPages <= 0) {
            document.body.style.overflowY = "auto";
            openPages = 0;
        }
    };

	var getIndex = function(pageId) {

		var actual = pages.find(item => item.id === pageId);
		var index = pages.indexOf(actual);
		return index;
	};

    this.RemovePage = function (pageId) {

		var index = getIndex(pageId);
        var toRemove = pages[index];
        pages.splice(index, 1);
		toRemove.element.remove();
		if( index > 0 )
        	openPage(--index);
    };

    this.CloseAll = function () {

		closePage(0);

		document.body.style.overflowY = "auto";
		openPages = 0
    };

	this.CreatePage = function(rootElement) {

        createPage();
        var addedPage = pages[pages.length - 1].element;
        $(addedPage).resizable({
			handles: 'w'
        });
		addedPage.querySelector("div[class=side-page-content]").append(rootElement);
	};

	(function() {

		pageManager = document.createElement('div');
		pageManager.style.position = 'fixed';
		pageManager.id = "side-pages-manager";
		document.body.append(pageManager);	
	})();
}