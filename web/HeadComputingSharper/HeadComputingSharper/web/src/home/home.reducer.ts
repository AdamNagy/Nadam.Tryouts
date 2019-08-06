import * as Actions from './home.actions';
import { MithrilActionWithPayload } from "../lib/mithril.action";
import { GameState, GameType } from "./home.models";


let initialState: GameState[] = [{

	level: 1,
	type: GameType.addition,
	left: "",
	right: "",
	solution: ""
}]

export function gameReducer(state:GameState[] = initialState, action: MithrilActionWithPayload) {

	// let newState = {  ...state };
	
	switch (action.type) {
		case Actions.NEXT_GAME:
			const newGame: GameState = {
				left: action.payload.left,
				right: action.payload.right,
				solution: action.payload.solution,
				level: 1,
				type: GameType.addition

			};
			const newState = [...state, newGame]; // .push(newGame);
			return newState;
		// case Actions.SET_LEVEL:
		// 	newState.level = action.payload.level;
		// 	break;
		default:
			return state;
	}

	// return newState;
}