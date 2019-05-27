export class RemovableElement {

	constructor(_element) {
        // this.element = _element;
        this.view = document.createElement("div");

        
        this.view.style.position = "relative";
        this.view.style.display = _element.style.display;

        var remover = document.createElement("div");
        remover.style.position = "absolute";
        remover.style.right = "10px";
        remover.style.top = "10px";
        remover.style.fontSize = "20px";
        remover.style.cursor = "pointer";
        remover.innerText = "X";

        remover.addEventListener("click", () => {

            this.view.remove();
        });
		this.view.append(remover);
		this.view.append(_element);
	}
};