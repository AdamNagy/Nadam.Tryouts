import { 
	combineReducers,
	createStore,
	applyMiddleware
} from 'redux';

import { todoReducer } from "./todo.store/reducers";

export const store = createStore(
	combineReducers({
		todos: todoReducer
	}));

function toObservable(store: any) {
	return {
		subscribe({ onNext }: any) {
		let dispose = store.subscribe(() => onNext(store.getState()));
		onNext(store.getState());
			return { dispose };
		}
	}
}
