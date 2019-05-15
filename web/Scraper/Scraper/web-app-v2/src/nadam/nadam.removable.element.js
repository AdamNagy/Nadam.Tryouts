export class RemovableElement {

	constructor(_element) {
        this.element = _element;
        this.item = document.createElement("div");

        this.item.append(_element);
        this.item.style.position = "relative";
        this.item.style.display = _element.style.display;

        var remover = document.createElement("div");
        remover.style.position = "absolute";
        remover.style.right = "10px";
        remover.style.top = "10px";
        remover.style.fontSize = "20px";
        remover.style.cursor = "pointer";
        remover.innerText = "X";

        remover.addEventListener("click", () => {

            this.item.remove();
        });
        this.element.append(remover);
	}
};