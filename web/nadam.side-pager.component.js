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
	var pageMargin = 50;
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

	var openPage = function(pageIdx) {

		var firstPageLeft = 0;
		for(var i = 0; i < pageIdx; ++i) {
			pages[i].style.right = firstPageLeft + "px";
			firstPageLeft -= pageMargin;
		}
	};

	this.DeletePage = function(pageIdx) {
		
	}

	var closePage = function(pageIdx) {
		
	}

	this.CloseAll = function() {

		var firstPageLeft = pages.length * pageMargin; 
		for(var i = 0; i < pages.length; ++i) {
			pages[i].style.right = "-" + (720 - firstPageLeft) + "px";
			firstPageLeft = firstPageLeft - pageMargin;
		}
	};

	this.CreatePage = function(rootElement) {

		createPage();
		var addedPage = pages[pages.length-1];
		addedPage.append(rootElement);
	};

	(function() {

		pageManager = document.createElement('div');
		pageManager.id = "side-pages-manager";
		document.body.append(pageManager);	
	})();
}