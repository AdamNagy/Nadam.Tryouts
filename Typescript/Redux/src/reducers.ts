import { combineReducers } from "redux";
import {
	ADD_TODO,
	SET_VISIBILITY_FILTER,
	TOGGLE_TODO,
	VisibilityFilters,
} from "./actions";

const { SHOW_ALL } = VisibilityFilters;

// visibility filter reducer
function visibilityFilter(state = SHOW_ALL, action: any) {
	switch (action.type) {
		case SET_VISIBILITY_FILTER:
			return action.filter;
		default:
			return state;
	}
}

// todo CRUD reducer
function todos(state: any[] = [], action: any) {
	switch (action.type) {
		case ADD_TODO:
			return [
				...state,
				{
					text: action.text,
					completed: false,
				},
			];
		case TOGGLE_TODO:
			return state.map((todo, index) => {
				if (index === action.index) {
					return Object.assign({}, todo, {
						completed: !todo.completed,
					});
				}
				return todo;
			});
		default:
			return state;
	}
}

const todoApp = combineReducers({
	visibilityFilter,
	todos,
});

export default todoApp;
