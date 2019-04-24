"use strict";

window.Nadam = window.Nadam || {}

Nadam.SidePager = function() {

    var self = this;
	var pageManager;
	var pages = new Array();

	var createPage = function() {

		var pageElement = document.createElement('div');

		pageElement.classList.add("side-page");
		pageElement.style.zIndex = 10 + pages.length;

		var opener = document.createElement("div");
		opener.style.height = "100%";
		opener.style.width = "30px"

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
	
	var openPage = function(pageIdx) {
		
		var firstPageLeft = 0;
		for(var i = 0; i <= pageIdx; ++i) {
			pages[i].style.right = firstPageLeft + "px";
			firstPageLeft -= 50;
		}
	};	

    var closePage = function (pageIdx) {

        var firstPageLeft = pages.length * 50;
        for (var i = pageIdx; i < pages.length; ++i) {
            pages[i].style.right = "-" + (720 - firstPageLeft) + "px";
            firstPageLeft = firstPageLeft - 50;
        }
    }

	this.CloseAll = function() {
		var firstPageLeft = pages.length * 50; 
		for(var i = 0; i < pages.length; ++i) {
			pages[i].style.right = "-" + (720 - firstPageLeft) + "px";
			firstPageLeft = firstPageLeft - 50;
		}
	};

	this.CreatePage = function(rootElement) {

		createPage();
		var addedPage = pages[pages.length-1];
		addedPage.append(rootElement);
	};

	(function() {
		pageManager = document.createElement('div');
		pageManager.style.position = 'fixed';
		pageManager.id = "side-pages-manager";
		document.body.append(pageManager);	
	})();
}