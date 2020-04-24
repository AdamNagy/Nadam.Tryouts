
function bindClick(target: any, key: any, descriptor: any): any {
	console.log({target});
	console.log({key});
	console.log({descriptor});
	return descriptor;
}

function bindValue(target: any, key: any): any {
	console.log({target});
	console.log({key});
}

class TestElement extends HTMLElement {

	private button: HTMLElement;
	@bindValue
	private nameInput: HTMLInputElement;
	private xprop = "hello";

	constructor() {
		super();

		this.button = document.createElement("button");
		this.button.innerText = "click it";
		this.append(this.button);

		this.nameInput = document.createElement("input");
		this.nameInput.setAttribute("type", "text");
		this.nameInput.setAttribute("id", "nameInput");
		this.nameInput.value = "add something";
		this.append(this.nameInput);
	}

	@bindClick
	public myButton(targetElement = ""): void {
		console.log("clicked");
	}

}

customElements.define("test-element", TestElement);
document.body.append(new TestElement());
