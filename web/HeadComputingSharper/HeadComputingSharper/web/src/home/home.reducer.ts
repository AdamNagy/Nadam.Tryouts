import * as Actions from './home.actions';
import { MithrilActionWithPayload } from "../lib/mithril.action";
import { GameState, GameType } from "./home.models";


let initialState: GameState[] = []

export function gameReducer(state:GameState[] = initialState, action: MithrilActionWithPayload) {

	switch (action.type) {
		case Actions.NEXT_GAME:
			const newGame: GameState = {
				left: action.payload.left,
				right: action.payload.right,
				solution: action.payload.solution,
				level: 1,
				type: GameType.addition

			};
			const newState = [...state, newGame];
			return newState;
		default:
			return state;
	}
}