import "./removable.element.scss";

export class RemovableElement extends HTMLElement {

	// public View: HTMLElement = document.createElement("div");
	private child: HTMLElement;
	private TemplateId = "template-removable";

	get Child() { 
		return this.child
	};

	constructor(item: HTMLElement) {
		
		super();
		this.child = item;
		// this.View.style.position = "relative";
		// this.View.style.display = element.style.display;

		const template = document.getElementById(this.TemplateId);
		const view = document.importNode((template as any).content, true);
		const remover: HTMLElement = view.querySelector("div.remover");//document.createElement("div");

		// remover.style.position = "absolute";
		// remover.style.right = "10px";
		// remover.style.top = "10px";
		// remover.style.fontSize = "20px";
		// remover.style.cursor = "pointer";
		// remover.innerText = "X";

		remover.addEventListener("click", () => {
			this.FireOnRemove(this)
			this.remove();
		});
		
		view.querySelector("div.removable").appendChild(item)
		this.appendChild(view);
	}

	// OnRemove event
	private onRemove: any[] = [];
	OnRemove(func: any) {
		this.onRemove.push(func);
	}

	private FireOnRemove(element: RemovableElement) {
		for(let func of this.onRemove) {
			func(element);
		}
	}
}

customElements.define('nadam-removable', RemovableElement);