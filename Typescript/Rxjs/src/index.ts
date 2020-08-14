import { fromEvent, merge, of } from "rxjs";
import { BehaviorSubject } from "rxjs/BehaviorSubject";

import { Action, AppStore, StateChange } from "./my-store";

// state and store building
class TodoModel {
	public content: string = "";
	public completed: boolean = false;
}

const initialTodoState: TodoModel[] =
	 [
		{
			completed: false,
			content: "task 1",
		},
		{
			completed: false,
			content: "task 2",
		},
	];

const store: AppStore = new AppStore(
	[
		{
			propName: "todos",
			reducerFunc: todoReducer,
		},
	]);

// html dom building
function renderTodos(todos: TodoModel[]) {
	const container = document.getElementById("todo-container");
	container.innerHTML = "";

	for (const todo of todos) {
		const todoItem = document.createElement("p");
		todoItem.innerText = todo.content;

		container.append(todoItem);
	}
}

const todoActions = {
	add: "todo:Add",
	delete: "todo:DELETE",
	filter: "todo:filter",
	markCompleted: "todo:MARK_COMPLETED",
};

enum todoFilter {
	all, copleted, notCompleted,
}

function todoReducer(state: TodoModel[] = initialTodoState, action: Action) {

	if ( action === undefined )
		return state;

	let newState: TodoModel[] = state;

	switch (action.type) {

		case todoActions.add:
			newState = [...state, action.payload];
			break;

		default: break;
	}

	return newState;
}

const todoInput: HTMLInputElement = document.createElement("input");
todoInput.setAttribute("type", "text");

const addTodoBtn = document.createElement("button");
addTodoBtn.innerText = "add todo";
addTodoBtn.addEventListener("click", () => {
	const content = todoInput.value;
	const newTodo: TodoModel = {
		completed: false,
		content,
	};

	store.dispatch(todoActions.add, newTodo);
});

const todoContainer = document.createElement("div");
todoContainer.classList.add("container");
todoContainer.setAttribute("id", "todo-container");

document.body.append(todoInput);
document.body.append(addTodoBtn);
document.body.append(todoContainer);

store.select("todos").subscribe((stateChange: StateChange<TodoModel[]>) => {
	renderTodos(stateChange.currentValue);
});
