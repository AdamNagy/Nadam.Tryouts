// v.1.0.1

"use strict";

/// usage:
/// var pageManager = new SidePager();
///	pageManager.CreatePage(document.createElement("div").innerText = "Hello world");

class SidePagerElement extends HTMLElement {

	pages = new Array();
	domParser = new DOMParser();
	openPages = 0;
	nextPageId = 1;

	pageWidth = 720;
	spaceBetweenPages = 30;

	template = 
		`<div class="side-page">
			<button nid='btn-close'>Close</button>
            <button nid='btn-remove'>Remove</button>
			<div class="side-page-opener"></div>
			<div class="side-page-content"></div>
		</div>`;

	constructor(_config) {
		
		super();	
	}

	// runs each time the element is added to the DOM
	connectedCallback() {
		var pages = this.querySelectorAll(".side-page");
		for(var page of pages) {
			var sidePage = this.createPageSkeleton();

			while( page.children.length > 0 ) {

				sidePage.querySelector(".side-page-content").append(page.children[0]);
			}
			page.remove();
			this.appendChild(sidePage);
		}

		this.style.display = "block";
		this.closeAll();
	}

	createPageSkeleton() {
			
		var pageElement = this.domParser.parseFromString(this.template, "text/html")
									.querySelector("div:first-child");

		pageElement.style.zIndex = 11 + this.nextPageId;		

		pageElement.querySelector("div[class='side-page-opener']")
			.addEventListener("click", ((pageId) => () => { this.openPage(pageId) })(this.nextPageId) );
		
		pageElement.querySelector("button[nid='btn-close']")
			.addEventListener("click", ((pageId) => () => { this.closePage(pageId) })(this.nextPageId) );
		
		pageElement.querySelector("button[nid='btn-remove']")
			.addEventListener("click", ((pageId) => () => { this.removePage(pageId) })(this.nextPageId) );

		this.pages.push({ id: this.nextPageId, element: pageElement });
		this.nextPageId += 1;
		return pageElement;
	};

	openPage(pageId) {

		var index = this.getIndexByPageId(pageId);
		if( index < 0 )
			return;

		var closedPagesWidth = (this.pages.length - 1 - index) * this.spaceBetweenPages;
		var openPages_buffer = index * this.spaceBetweenPages;

        var pageRightPosition = closedPagesWidth + openPages_buffer;
        for (var i = 0; i <= index; ++i) {

            var currentElement = this.pages[i];
            currentElement.element.style.right = pageRightPosition + "px";
            currentElement.element.classList.add("side-page-open");
            currentElement.element.classList.remove("side-page-closed");
            pageRightPosition -= this.spaceBetweenPages;
        }

        document.body.style.overflowY = "hidden";
        this.openPages++;
    };

    closePage(pageId) {

		var index = this.getIndexByPageId(pageId);
		if( index < 0 )
			return;

		var buffer = this.pages.length - index - 1; 
        var pageRightPosition = (buffer * this.spaceBetweenPages) + this.spaceBetweenPages;
        for (var i = index; i < this.pages.length; ++i) {
            var actual = this.pages[i];
            actual.element.style.left = null;
            actual.element.style.width = this.pageWidth + "px";
			actual.element.style.right = "-" + (this.pageWidth - pageRightPosition) + "px";
			
            actual.element.classList.remove("side-page-open");
            actual.element.classList.add("side-page-closed");
            pageRightPosition -= this.spaceBetweenPages;
        }

        this.openPages--;

        if (this.openPages <= 0) {
            document.body.style.overflowY = "auto";
            this.openPages = 0;
        }
    };

	getIndexByPageId(pageId) {

		var actual = this.pages.find(item => item.id === pageId);

		if( !actual )
			return;

		return this.pages.indexOf(actual);
	};

    removePage(pageId) {

		var index = this.getIndexByPageId(pageId);
		if( index < 0 )
			return;

        var toRemove = this.pages[index];
        this.pages.splice(index, 1);
		toRemove.element.remove();

		if( index > 0 )
        	openPage(--index);
    };

    closeAll() {
		this.closePage(1);
    };

	addPage(contentElement) {

        var sidePage = this.createPageSkeleton();
		sidePage.querySelector("div[class=side-page-content]").append(contentElement);
		this.appendChild(sidePage);
		this.closeAll();
	};
}

customElements.define('ndm-side-pager', SidePagerElement);
