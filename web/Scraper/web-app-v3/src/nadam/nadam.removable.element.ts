export class RemovableElement {

	public View: HTMLElement = document.createElement("div");

	constructor(element: HTMLElement) {

		this.View.style.position = "relative";
		this.View.style.display = element.style.display;

		const remover: HTMLElement = document.createElement("div");
		remover.style.position = "absolute";
		remover.style.right = "10px";
		remover.style.top = "10px";
		remover.style.fontSize = "20px";
		remover.style.cursor = "pointer";
		remover.innerText = "X";

		remover.addEventListener("click", () => {
			this.View.remove();
		});
		this.View.append(remover);
		this.View.append(element);
	}
}