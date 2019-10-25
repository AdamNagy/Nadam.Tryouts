// v.1.0.1

// import "jquery-ui/ui/widgets/resizable";
import "./side-pager.element.scss";

interface IPage {

	id: number;
	element: HTMLElement;
}

/// usage:
/// var pageManager = new SidePager();
/// pageManager.CreatePage(document.createElement("div").innerText = "Hello world");
export class SidePagerElement {
	get View() {
		return this.view;
	}

	private view: HTMLElement;
	private PageManager = document.createElement("div");
	private Pages: Array<IPage>;
	private PageWidth = 720;
	private SpaceBetweenPages = 50;
	private Template =
		`<div class="side-page">
			<h3>#title#</h3>
			<button data-local-id='close-btn' class='btn btn-primary'>Close</button>
			<button data-local-id='remove-btn' class='btn btn-warning'>Remove</button>
			<div class="side-page-opener"></div>
			<div class="side-page-content"></div>
		</div>`;
		
	private OpenPages = 0;

	/* Events */
	private onAllClosed: any[] = [];

	private onAnyOpen: any[] = [];
	
	constructor() {

		this.PageManager.style.position = "fixed";
		this.PageManager.id = "side-pages-manager";

		this.view = this.PageManager;
		this.Pages = new Array();
	}

    OpenPage(pageId: number): void {

        var index: number = this.GetIndex(pageId);
        var closedPagesWidth: number = (this.Pages.length - 1 - index) * this.SpaceBetweenPages;
        var openPages_buffer: number = index * this.SpaceBetweenPages;

        var pageRightPosition: number = closedPagesWidth + openPages_buffer;
        for (let i: number = 0; i <= index; ++i) {

            var currentElement: IPage = this.Pages[i];
            currentElement.element.style.right = pageRightPosition + "px";
            currentElement.element.classList.add("side-page-open");
            currentElement.element.classList.remove("side-page-closed");
            pageRightPosition -= 50;
		}
		
		this.OpenPages = this.OpenPages + 1;
		this.FireOnAnyOpen();
    }

    ClosePage(pageId: number): void {

        var index: number = this.GetIndex(pageId);

        var buffer: number = this.Pages.length - index - 1;
        var pageRightPosition: number = buffer * 50 + 50;
        for (let i: number = index; i < this.Pages.length; ++i) {
            var actual: IPage = this.Pages[i];
            actual.element.style.left = null;
            actual.element.style.width = this.PageWidth + "px";
            actual.element.style.right = "-" + (720 - pageRightPosition) + "px";

            actual.element.classList.remove("side-page-open");
            actual.element.classList.add("side-page-closed");
            pageRightPosition -= 50;
		}
		
		this.OpenPages = this.OpenPages > 0 ? this.OpenPages - 1 : 0;
		if( this.OpenPages === 0 )
			this.FireOnAllClosed();
    }

    RemovePage(pageId: number): void {

        var index: number = this.GetIndex(pageId);
        var toRemove: IPage = this.Pages[index];
        this.Pages.splice(index, 1);
		toRemove.element.remove();
		
		this.OpenPages = this.OpenPages > 0 ? this.OpenPages - 1 : 0;
		if( this.OpenPages === 0 )
			this.FireOnAllClosed();
    }

    CloseAll(): void {

        this.ClosePage(0);
    }

    AddPage(rootElement: HTMLElement): void {

        this.CreatePage("");
		var addedPage: HTMLElement = this.Pages[this.Pages.length - 1].element;

		// $(addedPage).resizable({
		// 	handles: 'w',
		// 	stop: function( event, ui ) {

		// 		var $grid = $('.grid').masonry({

		// 			itemSelector: '.grid-item',
		// 			columnWidth: 200,
		// 			gutter: 5
		// 		});
		// 	}
		// });

        addedPage.querySelector("div[class=side-page-content]").append(rootElement);
	}
	OnAllClosed(func: any) {
		this.onAllClosed.push(func)
	}	
	OnAnyOpen(func: any) {
		this.onAnyOpen.push(func);
	}

	private CreatePage(title: string): void {

		if (title === undefined || title === "") {
			title = "";
		}

		const domParser: DOMParser = new DOMParser();
		const instanceTemplate: string = this.Template.replace("#title#", title);
		const pageElement: HTMLElement = domParser.parseFromString(instanceTemplate, "text/html")
												.querySelector("div:first-child") || document.createElement("div");

		pageElement.style.zIndex = (11 + this.Pages.length).toString();

		pageElement.querySelector("div[class='side-page-opener']")
			.addEventListener("click", ((idx) => {
				return () => { this.OpenPage(idx); };
			})(this.Pages.length));

		pageElement.querySelector("button[data-local-id='close-btn']")
			.addEventListener("click", ((idx) => { return () => { 
					this.ClosePage(idx); }; 
			})(this.Pages.length));

        pageElement.querySelector("button[data-local-id='remove-btn']")
            .addEventListener("click", ((idx) => {
                return () => { this.RemovePage(idx); };
            })(this.Pages.length));

        this.Pages.push({ id: this.Pages.length, element: pageElement } as IPage);
        this.PageManager.append(pageElement);
        this.CloseAll();
    }

    private GetIndex(pageId: number): number {

        var actual: IPage = this.Pages.find(item => item.id === pageId);
        var index: number = this.Pages.indexOf(actual);
        return index;
    }
	
	private FireOnAnyOpen() {
		for(let func of this.onAnyOpen) {
			func();
		}
	}

	private FireOnAllClosed() {
		for(let func of this.onAllClosed) {
			func();
		}
	}
}
