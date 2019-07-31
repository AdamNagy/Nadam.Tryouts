export interface MithrilAction {
	type: string;
	redraw: boolean;
}

export interface MithrilActionWithPayload extends MithrilAction {
	payload: any;
}