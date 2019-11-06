import { createStore } from 'redux';
import rootReducer from './reducers';
import {addTodo} from "./actions/index";
import { Main } from "./components/main.component";

window.store = createStore(rootReducer);

(function() {

	window.store.dispatch(addTodo('Learn about actions'));
	console.log(store);
	new Main();
})()