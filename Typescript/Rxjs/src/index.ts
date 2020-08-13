import { BehaviorSubject } from "rxjs/BehaviorSubject";
import { fromEvent, merge, of } from "rxjs";

import { AppStore, Action, StateChange } from "./my-store";

// state and store building
class TodoModel {
	content: string = "";
	completed: boolean = false;
}

var initialTodoState: TodoModel[] = 
	 [
		{
			content: "task 1",
			completed: false
		},
		{
			content: "task 2",
			completed: false
		}
	];

var store: AppStore = new AppStore(
	[
		{
			propName: "todos",
			reducerFunc: todoReducer
		}
	]);


// html dom building
function renderTodos(todos: TodoModel[]) {
	var container = document.getElementById("todo-container");
	container.innerHTML = "";

	for(var todo of todos) {
		var todoItem = document.createElement("p");
		todoItem.innerText = todo.content;

		container.append(todoItem);
	}
}

const todoActions = {
	add: "todo:Add",
	delete: "todo:DELETE",
	markCompleted: "todo:MARK_COMPLETED",
	filter: "todo:filter"
}

enum todoFilter {
	all, copleted, notCompleted
}

function todoReducer(state: TodoModel[] = initialTodoState, action: Action = undefined) {

	if( action === undefined )
		return state;

	let newState: TodoModel[] = state;

	switch(action.type) {
		
		case todoActions.add:
			newState = [...state, action.payload];
			break;

		default: break;
	}

	return newState;
}

var todoInput: HTMLInputElement = document.createElement("input");
todoInput.setAttribute("type", "text");

var addTodoBtn = document.createElement("button");
addTodoBtn.innerText = "add todo";
addTodoBtn.addEventListener("click", () => {
	var content = todoInput.value;
	var newTodo: TodoModel = {
		completed: false,
		content
	};

	store.dispatch(todoActions.add, newTodo);
});

var todoContainer = document.createElement("div");
todoContainer.classList.add("container");
todoContainer.setAttribute("id", "todo-container");

document.body.append(todoInput);
document.body.append(addTodoBtn);
document.body.append(todoContainer);

store.select("todos").subscribe((stateChange: StateChange<TodoModel[]>) => {
	renderTodos(stateChange.currentValue);
});
