import { MithrilAction } from "../lib/mithril.action";

export const INCREMENT_AGE = 'INCREMENT_AGE';
export const DECREMENT_AGE = 'DECREMENT_AGE';

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