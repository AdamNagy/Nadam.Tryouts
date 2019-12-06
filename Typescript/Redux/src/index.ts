import { createStore } from "redux";
import { addTodo } from "./actions";
import todoApp, { AppState } from "./reducers";

export const store = createStore(todoApp);

const inputTodo = document.getElementById("todo-input") as HTMLInputElement;
const btnAddTodo = document.getElementById("todo-add-btn");
const listTodo = document.getElementById("todo-list");

btnAddTodo.addEventListener("click", () => {
	store.dispatch(addTodo(inputTodo.value));
});

function todosSelector(state: AppState) {
	return state.todos;
}

let currentValue: any;
function handleChange() {
	const previousValue = currentValue;
	currentValue = todosSelector(store.getState());

	if (previousValue !== currentValue) {
		console.log(
			"Some deep nested property changed from",
			previousValue,
			"to",
			currentValue,
		);
	}
}

function reRender() {

	const previousList = listTodo.children;
	for (const item of previousList) {
		item.remove();
	}

	const updatedList = todosSelector(store.getState());
	for (const newItem of updatedList) {
		const listItem = document.createElement("li");
		listItem.innerText = newItem.text;
		listTodo.append(listItem);
	}
}

store.subscribe(handleChange);
store.subscribe(reRender);
