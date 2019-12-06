import { createStore } from "redux";
import { addTodo } from "./actions";
import todoApp, { AppState } from "./reducers";

export const store = createStore(todoApp);

const inputTodo = document.getElementById("todo-input") as HTMLInputElement;
const btnAddTodo = document.getElementById("todo-add-btn");

btnAddTodo.addEventListener("click", () => {
	console.log(inputTodo.getAttribute("value"));
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

const unsubscribe = store.subscribe(handleChange);
