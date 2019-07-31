import * as Actions from './home.actions';

export function age(state = 0, action) {

	let newState = {};
	switch (action.type) {
		case Actions.INCREMENT_AGE:
			newState = state + 1;
			break;
		case Actions.DECREMENT_AGE:
			newState = Math.max(0, state - 1);
			break;
		default:
			return state;
	}

	console.log("reducer func:" + newState);
	return newState;
}