import { TodoStateModel } from "./state-model";

export function getTodos(state: any): TodoStateModel[] {
	return state.getState().todos;
}