import { combineReducers } from 'redux';
import * as Actions from './actions';

function name(state = '', action) {
  return state;
}

function age(state = 0, action) {

	let newState = {};
	switch (action.type) {
		case Actions.INCREMENT_AGE:
			newState = state + 1;
			break;
		case Actions.DECREMENT_AGE:
			newState = Math.max(0, state - 1);
		case Actions.RESET_AGE:
			return action.age;
		default:
			return state;
	}
	console.log(newState);
	return newState;
}

const rootReducer = combineReducers({
  name,
  age
});

export default rootReducer;