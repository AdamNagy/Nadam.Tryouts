import {MithrilAction} from "./mithril.action";

export const INCREMENT_AGE = 'INCREMENT_AGE';
export const DECREMENT_AGE = 'DECREMENT_AGE';
export const RESET_AGE = 'RESET_AGE';

export function incrementAge(): MithrilAction {
	return {
		type: INCREMENT_AGE,
		redraw: false
	};	
}

export function decrementAge(): MithrilAction {
  return {
	type: DECREMENT_AGE,
	redraw: true
  };
}

export function resetAge() {
  return (dispatch) => {
    setTimeout(() => dispatch({
      type: RESET_AGE,
      redraw: true,
      age: 30
    }), 2000);
  };
}