import * as rx from "rxjs";

class MyDom {

	public observables: any = {};

	protected ROOT = "root";
	protected dom: any = {};

	constructor(element: HTMLElement) {

		this.dom[this.ROOT] = element;
		this.Scan(this.dom[this.ROOT]);
	}

	private Scan(root: HTMLElement) {
		for (const item of root.children) {
			const id = item.getAttribute("id");
			if ( id === undefined )
				continue;

			this.dom[id] = item;

			if ( item.nodeName.toLowerCase() === "button" )
				this.observables[id] = rx.fromEvent(item, "click");

			this.Scan(item as HTMLElement);
		}
	}
}

const main = document.getElementById("main");
const mainDom = new MyDom(main);
// tslint:disable-next-line: no-console
mainDom.observables["my-btn"].subscribe(() => console.log("Clicked!"));
