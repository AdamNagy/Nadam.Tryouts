import { BehaviorSubject } from "rxjs/BehaviorSubject";

export class ReducableProperty {
	public propName: string;
	public reducerFunc: any;
}

export class StateProperty extends ReducableProperty {
	public subject: BehaviorSubject<any>;
	public actions: Action[];
}

export class Action {

	constructor(_type: string) {
		this.type = _type;
	}

	public type: string = "";
	public payload: any = {};
}

export class StateChange<T> {
	public lastAction: Action;
	public currentValue: any;
}
export class AppStore {

	private stateQueue: any[] = [];
	private stateProperties: StateProperty[] = [];

	constructor(_reducables: ReducableProperty[]) {

		let initialState = {};
		for (const reducable of _reducables) {
			const newProp: any = {};
			newProp[reducable.propName] =  reducable.reducerFunc();

			initialState = Object.assign({}, initialState, newProp);

			this.stateProperties.push({
				propName: reducable.propName,
				reducerFunc: reducable.reducerFunc,
				subject: new BehaviorSubject<StateChange<any>>({ lastAction: undefined, currentValue: newProp[reducable.propName] }),
				actions: [],
			});
		}

		this.stateQueue.push(initialState);
	}

	public getState() {
		return this.stateQueue[this.stateQueue.length - 1];
	}

	public pushState(state: any) {
		this.stateQueue.push(state);
	}

	public dispatch(action: string, payload: any) {

		const actionWithPayload: Action = {type: action, payload};
		const propertyName: string = action.split(":")[0];

		const currentState: any = this.getState();
		const stateProperty = this.stateProperties.filter((p) => p.propName === propertyName)[0];

		const newSubState = stateProperty.reducerFunc(currentState[propertyName], actionWithPayload);

		const newStatePropertyValue: any = {};
		newStatePropertyValue[propertyName] = newSubState;

		this.pushState(Object.assign({}, currentState, newStatePropertyValue));
		stateProperty.actions.push(actionWithPayload);
		stateProperty.subject.next({lastAction: actionWithPayload, currentValue: newStatePropertyValue[propertyName]} as StateChange<any>);
	}

	public select(propertyName: string): BehaviorSubject<StateChange<any>> {
		const stateProperty = this.stateProperties.filter((p) => p.propName === propertyName)[0];
		return stateProperty.subject;
	}
}
