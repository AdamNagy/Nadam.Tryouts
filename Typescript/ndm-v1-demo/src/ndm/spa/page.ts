
export class Page extends HTMLElement {

	get Route() {
		return this.getAttribute("route");
	}

	constructor(route: string) {
		super();

		this.setAttribute("route", route);
	}

	public Show() {
		$(this).show();
	}

	public Hide() {
		$(this).hide();
	}
}

customElements.define("ndm-page", Page);
