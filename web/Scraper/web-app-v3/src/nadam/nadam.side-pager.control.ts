// v.1.0.1

import 'jquery-ui/ui/widgets/resizable';
import './nadam.side-pager.control.css';

/// usage:
/// var pageManager = new SidePager();
///	pageManager.CreatePage(document.createElement("div").innerText = "Hello world");
export class SidePager {

	constructor() {

		this.pageManager = document.createElement('div');
		this.pageManager.style.position = 'fixed';
		this.pageManager.id = "side-pages-manager";
		document.body.append(this.pageManager);

		this.pages = new Array();
		this.domParser = new DOMParser();
		this.openPages = 0;
	
		this.pageWidth = 720;
		this.spaceBetweenPages = 50;

		this.template =
			`<div class="side-page">
				<h3>#title#</h3> 
				<button data-local-id='close-btn'>Close</button>
				<button data-local-id='remove-btn'>Remove</button>
				<div class="side-page-opener"></div>
				<div class="side-page-content"></div>
			</div>`;
	}

    createPage(title) {

        if (title === undefined)
            title = "";

        var instanceTemplate = this.template.replace("#title#", title);
        var pageElement = this.domParser.parseFromString(instanceTemplate, "text/html")
            .querySelector("div:first-child");
        pageElement.style.zIndex = 11 + this.pages.length;

        pageElement.querySelector("div[class='side-page-opener']")
            .addEventListener("click", ((idx) => {
                return () => { this.openPage(idx); };
            })(this.pages.length));

        pageElement.querySelector("button[data-local-id='close-btn']")
            .addEventListener("click", ((idx) => {
                return () => { this.closePage(idx); };
            })(this.pages.length));

        pageElement.querySelector("button[data-local-id='remove-btn']")
            .addEventListener("click", ((idx) => {
                return () => { this.RemovePage(idx); };
            })(this.pages.length));

        this.pages.push({ id: this.pages.length, element: pageElement });
        this.pageManager.append(pageElement);
        this.CloseAll();
    }

    openPage(pageId) {

        var index = this.getIndex(pageId);
        var closedPagesWidth = (this.pages.length - 1 - index) * this.spaceBetweenPages;
        var openPages_buffer = index * this.spaceBetweenPages;

        var pageRightPosition = closedPagesWidth + openPages_buffer;
        for (var i = 0; i <= index; ++i) {

            var currentElement = this.pages[i];
            currentElement.element.style.right = pageRightPosition + "px";
            currentElement.element.classList.add("side-page-open");
            currentElement.element.classList.remove("side-page-closed");
            pageRightPosition -= 50;
        }

        // document.body.style.overflowY = "hidden";
		this.openPages++;
    }

    closePage(pageId) {

        var index = this.getIndex(pageId);

        var buffer = this.pages.length - index - 1;
        var pageRightPosition = buffer * 50 + 50;
        for (var i = index; i < this.pages.length; ++i) {
            var actual = this.pages[i];
            actual.element.style.left = null;
            actual.element.style.width = this.pageWidth + "px";
            actual.element.style.right = "-" + (720 - pageRightPosition) + "px";

            actual.element.classList.remove("side-page-open");
            actual.element.classList.add("side-page-closed");
            pageRightPosition -= 50;
        }

        this.openPages--;

        if (this.openPages <= 0) {
            // document.body.style.overflowY = "auto";
            this.openPages = 0;
        }
    }

    getIndex(pageId) {

        var actual = this.pages.find(item => item.id === pageId);
        var index = this.pages.indexOf(actual);
        return index;
    }

    RemovePage(pageId) {

        var index = this.getIndex(pageId);
        var toRemove = this.pages[index];
        this.pages.splice(index, 1);
        toRemove.element.remove();
        --this.openPages;
        if (index > 0)
            this.openPage(--index);
    }

    CloseAll() {

        this.closePage(0);

        // document.body.style.overflowY = "auto";
        this.openPages = 0;
    }

    CreatePage(rootElement) {

        this.createPage();
		var addedPage = this.pages[this.pages.length - 1].element;
		
		$(addedPage).resizable({
			handles: 'w',
			stop: function( event, ui ) {

				var $grid = $('.grid').masonry({

					itemSelector: '.grid-item',
					columnWidth: 200,
					gutter: 5
				});

				// $grid.masonry();
			}
		});

        addedPage.querySelector("div[class=side-page-content]").append(rootElement);
	}
}