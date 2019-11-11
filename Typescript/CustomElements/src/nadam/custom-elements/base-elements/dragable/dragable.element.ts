import "jquery-ui/ui/widgets/draggable";

export class DragableElement {

	public view: HTMLElement;
	get View() {
		return this.view;
	}

	constructor() {

		this.view = document.createElement("div");
		this.view.innerText = "hello from draggable element";
		$(this.view).draggable();
	}
}
