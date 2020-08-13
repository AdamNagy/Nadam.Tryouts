import { fromEvent, merge, of } from "rxjs";
import "rxjs/add/operator/map";
import "rxjs/add/operator/merge";
import "rxjs/add/operator/scan";
import { Subject } from "rxjs/Subject";

// state and store building
class AppState {
	todos: TodoModel[] = [];
}

class StateReducer {
	propName: string;
	reducerFunc: any;
}

class Action {

	constructor(_type: string, reducers: StateReducer[]) {
		this.type = _type;
	}

	type: string = "";
	payload: any = {};
}

class AppStore {

	private stateQueue: AppState[] = [];
	private reducers: StateReducer[] = [];

	constructor(_reducers: StateReducer[]) {

		this.reducers = _reducers;

		var nullState = {};
		for(var reducer of this.reducers) {
			var newProp: any = {};
			newProp[reducer.propName] =  reducer.reducerFunc();

			nullState = Object.assign({}, nullState, newProp)
		}

		this.stateQueue.push(nullState as AppState);
	}

	public getState(): AppState {
		return this.stateQueue[this.stateQueue.length - 1];
	}

	public pushState(state: AppState) {
		this.stateQueue.push(state);
	}

	public dispatch(action: string, payload: any) {

		var propertyName: string = action.split(':')[0];

		var currentState: any = this.getState();
		var reducer = this.reducers.filter(p => p.propName === propertyName)[0];

		var newSubState = reducer.reducerFunc(currentState[propertyName], {type: action, payload} as Action);

		var p: any = {};
		p[propertyName] = newSubState;

		this.pushState(Object.assign({}, currentState, p));
	}
}

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


function todoSelector(state: AppState) {

	const stateSubject = new Subject<TodoModel[]>();
	stateSubject.next(state.todos);
	return stateSubject;
}

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

function todoReducer(state: TodoModel[] = initialTodoState, action: Action = {type: "", payload: undefined}) {

	var newState: TodoModel[] = state;

	switch(action.type) {
		case "todos:ADD":
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

	store.dispatch("todos:ADD", newTodo);
	// renderTodos(todoSelector(store.getState()));
});

var todoContainer = document.createElement("div");
todoContainer.classList.add("container");
todoContainer.setAttribute("id", "todo-container");

document.body.append(todoInput);
document.body.append(addTodoBtn);
document.body.append(todoContainer);

// renderTodos( todoSelector(store.getState()) );
