import { TodoStateModel, initialState } from "./state-model";
import { ADD_TODO } from "./actions";

export function todoReducer(state: TodoStateModel[] = initialState, action: any) {

	switch(action.type) {
		case ADD_TODO:
			return [
				...state,
				action.payload
			];
			
		default: return state;
	}
}