/*
 * action types
 */

export const ADD_TODO = "ADD_TODO";
export const TOGGLE_TODO = "TOGGLE_TODO";
export const SET_VISIBILITY_FILTER = "SET_VISIBILITY_FILTER";

/*
 * other constants
 */

export const VisibilityFilters = {
	SHOW_ACTIVE: "SHOW_ACTIVE",
	SHOW_ALL: "SHOW_ALL",
	SHOW_COMPLETED: "SHOW_COMPLETED",
};

/*
 * action creators
 */

export function addTodo(text: string) {
	return {
		text,
		type: ADD_TODO,
	};
}

export function toggleTodo(index: number) {
	return {
		index,
		type: TOGGLE_TODO,
	};
}

export function setVisibilityFilter(filter: string) {
	return {
		filter,
		type: SET_VISIBILITY_FILTER,
	};
}
