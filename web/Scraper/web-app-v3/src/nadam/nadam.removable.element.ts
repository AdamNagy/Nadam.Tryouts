export class RemovableElement extends HTMLElement {

	constructor(element: HTMLElement) {

		super();

		var shadow = this.attachShadow({mode: 'open'});

		var view = document.createElement('span');
		view.style.position = "relative";
		view.style.display = element.style.display;

		const remover: HTMLElement = document.createElement("div");
		remover.style.position = "absolute";
		remover.style.right = "10px";
		remover.style.top = "10px";
		remover.style.fontSize = "20px";
		remover.style.cursor = "pointer";
		remover.innerText = "X";

		remover.addEventListener("click", () => {
			view.remove();
		});
		view.append(remover);
		view.append(element);

		var style = document.createElement('style');

		shadow.appendChild(style);
		shadow.appendChild(view);
	}
}