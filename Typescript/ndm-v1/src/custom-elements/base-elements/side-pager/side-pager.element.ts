import { PrototypeHTMLElement } from "../../../htmlelement/prototype-html-element";
import "./side-pager.element.scss";

interface IPage {

	id: number;
	element: HTMLElement;
}

export class SidePagerElement extends PrototypeHTMLElement {

	private TemplateId = "template-side-pager";
	private Pages: Array<IPage>;
	private PageWidth = 720;
	private SpaceBetweenPages = 50;

	private OpenPages = 0;

	/* Events */
	private onAllClosed: any[] = [];
	private onAnyOpen: any[] = [];

	constructor() {

		super();

		this.WithStyle("position", "fixed")
			.WithId("side-pages-manager");

		this.Pages = new Array();
	}

	public OpenPage(pageId: number): void {

		const index: number = this.GetIndex(pageId);
		const closedPagesWidth: number = (this.Pages.length - 1 - index) * this.SpaceBetweenPages;
		const openPagesBuffer: number = index * this.SpaceBetweenPages;

		let pageRightPosition: number = closedPagesWidth + openPagesBuffer;
		for (let i: number = 0; i <= index; ++i) {

			const currentElement: IPage = this.Pages[i];
			currentElement.element.style.right = pageRightPosition + "px";
			currentElement.element.classList.add("side-page-open");
			currentElement.element.classList.remove("side-page-closed");
			pageRightPosition -= 50;
		}

		this.OpenPages = this.OpenPages + 1;
		this.FireOnAnyOpen();
	}

	public ClosePage(pageId: number): void {

		const index: number = this.GetIndex(pageId);

		const buffer: number = this.Pages.length - index - 1;
		let pageRightPosition: number = buffer * 50 + 50;
		for (let i: number = index; i < this.Pages.length; ++i) {
			const actual: IPage = this.Pages[i];
			actual.element.style.left = null;
			actual.element.style.width = this.PageWidth + "px";
			actual.element.style.right = "-" + (720 - pageRightPosition) + "px";

			actual.element.classList.remove("side-page-open");
			actual.element.classList.add("side-page-closed");
			pageRightPosition -= 50;
		}

		this.OpenPages = this.OpenPages > 0 ? this.OpenPages - 1 : 0;
		if ( this.OpenPages === 0 )
			this.FireOnAllClosed();
	}

	public RemovePage(pageId: number): void {

		const index: number = this.GetIndex(pageId);
		const toRemove: IPage = this.Pages[index];
		this.Pages.splice(index, 1);
		toRemove.element.remove();

		this.OpenPages = this.OpenPages > 0 ? this.OpenPages - 1 : 0;
		if ( this.OpenPages === 0 )
			this.FireOnAllClosed();
	}

	public CloseAll(): void {
		this.ClosePage(0);
	}

	public AddPage(rootElement: HTMLElement): SidePagerElement {

		this.CreatePage("");
		const addedPage: HTMLElement = this.Pages[this.Pages.length - 1].element;

		addedPage.querySelector("div[class=side-page-content]").append(rootElement);
		return this;
	}

	public OnAllClosed(func: any) {
		this.onAllClosed.push(func);
	}

	public OnAnyOpen(func: any) {
		this.onAnyOpen.push(func);
	}

	private CreatePage(title: string): void {

		if (title === undefined || title === "") {
			title = "";
		}

		const template = document.getElementById(this.TemplateId);
		const node = document.importNode((template as any).content, true);

		const pageElement: HTMLElement = node.querySelector("div:first-child") || document.createElement("div");

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
		this.append(pageElement);
		this.CloseAll();
	}

	private GetIndex(pageId: number): number {

		const actual: IPage = this.Pages.find((item) => item.id === pageId);
		const index: number = this.Pages.indexOf(actual);
		return index;
	}

	private FireOnAnyOpen() {
		for (const func of this.onAnyOpen) {
			func();
		}
	}

	private FireOnAllClosed() {
		for (const func of this.onAllClosed) {
			func();
		}
	}
}

customElements.define("ndm-side-pager", SidePagerElement);