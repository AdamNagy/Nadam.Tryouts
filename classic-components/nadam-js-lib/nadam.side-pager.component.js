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

	var spaceBetweenPages = 50;
	var pageWidth = 720;

	var template = 
		`<div class="side-page">
			<h3>#title#</h3> 
			<button>Close</button>
			<div class="side-page-opener"></div>
			<div class="side-page-content"></div>
		</div>`;

	var createPage = function(title) {

		if( title === undefined )
			title = "";
			
		var instanceTemplate = template.replace("#title#", title);
		var pageElement = domParser.parseFromString(instanceTemplate, "text/html")
									.querySelector("div:first-child");
		pageElement.style.zIndex = 10 + pages.length;		

		pageElement.querySelector("div[class='side-page-opener']")
			.addEventListener("click", (function (idx) {
				return function () {
					openPage(idx);
				};
			})(pages.length));

		pageElement.querySelector("button")
			.addEventListener("click", (function (idx) {
				return function () {
					closePage(idx);
				};
			})(pages.length));

		pages.push(pageElement);
		pageManager.append(pageElement);
		self.CloseAll();
	};

	var openPage = function (pageIdx) {

		if( pages[pageIdx].classList.contains("side-page-open") )
			return;

        var pageRightPosition = pageIdx > 0 ? (pageIdx)*50 : 0;
        for (var i = 0; i <= pageIdx; ++i) {
            pages[i].style.right = pageRightPosition + "px";
            pages[i].classList.add("side-page-open");
            pages[i].classList.remove("side-page-closed");
            pageRightPosition -= 50;
        }
        document.body.style.overflowY = "hidden";
        openPages++;
    };

    var closePage = function (pageIdx) {

        var pageRightPosition = pages.length * 50;
        for (var i = pageIdx; i < pages.length; ++i) {
            pages[i].style.right = "-" + (720 - pageRightPosition) + "px";
            pages[i].classList.remove("side-page-open");
            pages[i].classList.add("side-page-closed");
            pageRightPosition -= 50;
		}
		
		openPages--;
		
        if (openPages <= 0) {
            document.body.style.overflowY = "auto";
			openPages = 0;
		}
    }

    this.CloseAll = function () {

		closePage(0);

		document.body.style.overflowY = "auto";
		openPages = 0
    };

	this.CreatePage = function(rootElement) {

		createPage();
		var addedPage = pages[pages.length-1];
		addedPage.querySelector("div[class=side-page-content]").append(rootElement);
	};

	(function() {

		pageManager = document.createElement('div');
		pageManager.style.position = 'fixed';
		pageManager.id = "side-pages-manager";
		document.body.append(pageManager);	
	})();
}