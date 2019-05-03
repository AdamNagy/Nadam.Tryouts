import { addTodo } from "../actions/index";

export class Main {

	constructor() {
		var btn = document.createElement("button");
		btn.addEventListener("click", (event) => {
			window.store.dispatch(addTodo("csumika"));
		});
		btn.innerText = "Component button";
		document.body.append(btn);
	}
}