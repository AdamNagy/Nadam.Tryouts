"use strict";

/// usage:
/// var pageManager = new SidePager();
///	pageManager.CreatePage(document.createElement("div").innerText = "Hello world");
window.Nadam = window.Nadam || {}; 
Nadam.SidePager = function() {

	var elementName = "side-page";

	var self = this;
	var pageManager;
	var pages = new Array();
	var domParser = new DOMParser();
	var pageMargin = 50;
	var pageWidth = 720;

	var template = 
		`<side-page>
			<h3>#title#</h3> 
			<button>Close</button>
			<div class="side-page-opener"></div>
			<div class="side-page-content"></div>
		</side-page>`;

	var initSidePage = function(sidePageElement, title) {

		if( title === undefined )
			title = "";
			
		var instanceTemplate = template.replace("#title#", title);
		// var sidePageElement = document.createElement(elementName);
		var pageElementControls = domParser.parseFromString(instanceTemplate, "text/html")
											.querySelector(elementName)
											.children;

		sidePageElement.style.zIndex = 10 + pages.length;		

		while(pageElementControls.length > 0) {
			sidePageElement.append(pageElementControls[0]);
		}

		sidePageElement.querySelector("div[class='side-page-opener']")
			.addEventListener("click", (function (idx) {
				return function () {
					openPage(idx);
				};
			})(pages.length));

		sidePageElement.querySelector("button")
			.addEventListener("click", (function (idx) {
				return function () {
					closePage(idx);
				};
			})(pages.length));

		pages.push(sidePageElement);
		pageManager.append(sidePageElement);
		self.CloseAll();

		return sidePageElement;
	};

	var createFromElement = function(sidePageElement) {
		
		var content = sidePageElement.children;
		var sidePageElementContent = initSidePage(sidePageElement, sidePageElement.getAttribute("title"))
					.querySelector("div[class='side-page-content']");

		while( sidePageElementContent.length > 0 ) {
			sidePageElement.append(content[0]);
		}
	}

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

	this.CreateDynamically = function(content, title) {

		var sidePageElement = initSidePage(document.createElement(elementName), title)
		.querySelector("div[class='side-page-content']");;

		if( content.length !== undefined ) {

			while( content.length > 0 ) {
				sidePageElement.append(content[0]);
			}
		}
	};

	(function() {
		pageManager = document.createElement('div');
		var definedSidePages = document.getElementsByTagName(elementName);

		for(var i = 0; i < definedSidePages.length; ++i) {
			createFromElement(definedSidePages[i]);
		}
	})();
}
