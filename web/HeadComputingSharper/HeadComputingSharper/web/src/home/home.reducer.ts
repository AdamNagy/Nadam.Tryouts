import * as Actions from './home.actions';
import { MithrilAction, MithrilActionWithPayload } from "../lib/mithril.action";

export enum GameType {
	addition, subtraction, multiplication, divsion, dates
} 

export interface GameState {

	level: number;
	type: GameType;
	left: string;
	right: string;
	solution: string;
}

let initialState: GameState = {

	level: 1,
	type: GameType.addition,
	left: "",
	right: "",
	solution: ""
}

export function gameReducer(state:GameState = initialState, action: MithrilActionWithPayload) {

	let newState = {  ...state };

	switch (action.type) {
		case Actions.NEXT_GAME:
			newState.left = action.payload.left;
			newState.right = action.payload.right;
			newState.solution = action.payload.solution;
			break;
		case Actions.SET_LEVEL:
			newState.level = action.payload.level;
			break;
		default:
			return state;
	}

	console.log("reducer func:" + newState);
	return newState;
}