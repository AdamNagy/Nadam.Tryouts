import { MithrilActionWithPayload } from "../lib/mithril.action";

export const NEXT_GAME = 'NEXT_GAME';
export const SET_LEVEL = 'SET_LEVEL';
export const SET_GAMETYPE = 'SET_GAMETYPE';

export function nextGame(payload): MithrilActionWithPayload {
	return {
		type: NEXT_GAME,
		redraw: true,
		payload: payload
	};	
}

export function setLevel(payload): MithrilActionWithPayload {
	return {
	  type: SET_LEVEL,
	  redraw: true,
	  payload: payload
	};
}

export function setGameType(payload): MithrilActionWithPayload {
	return {
		type: SET_GAMETYPE,
		redraw: false,
		payload: payload
	}
}