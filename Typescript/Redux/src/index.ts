import { addTodo } from "./todo.store/actions";
import { store } from "./store";
import { getTodos } from "./todo.store/selectors";
import { TodoStateModel } from "./todo.store/state-model";

var todos = getTodos(store);

for(var post of todos) {
	var title = document.createElement("p");
	title.innerText = post.text

	document.body.append(title);
}

var todoTitleInput = document.getElementById("todo-title-input") as HTMLInputElement;
var todoAddBtn = document.getElementById("todo-add-btn");

todoAddBtn.addEventListener("click", () => {

	var title = todoTitleInput.value;

	var newTodo: TodoStateModel = {
		conpleted: false,
		text: title
	};

	store.dispatch(addTodo(newTodo));
	
	store.subscribe(() => {
		console.log({ todos: getTodos(store) });
	})
});
