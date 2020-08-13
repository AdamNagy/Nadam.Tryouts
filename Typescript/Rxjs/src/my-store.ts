import { BehaviorSubject } from "rxjs/BehaviorSubject";

export class ReducableProperty {
	propName: string;
	reducerFunc: any;
}

export class StateProperty extends ReducableProperty {
	subject: BehaviorSubject<any>;
	actions: Action[];
}

export class Action {

	constructor(_type: string) {
		this.type = _type;
	}

	type: string = "";
	payload: any = {};
}

export class StateChange<T> {
	lastAction: Action;
	currentValue: any;
}
export class AppStore {

	private stateQueue: any[] = [];
	private stateProperties: StateProperty[] = [];

	constructor(_reducables: ReducableProperty[]) {

		var initialState = {};
		for(var reducable of _reducables) {
			var newProp: any = {};
			newProp[reducable.propName] =  reducable.reducerFunc();

			initialState = Object.assign({}, initialState, newProp);

			this.stateProperties.push({
				propName: reducable.propName,
				reducerFunc: reducable.reducerFunc,
				subject: new BehaviorSubject<StateChange<any>>({ lastAction: undefined, currentValue: newProp[reducable.propName] }),
				actions: []
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

		let actionWithPayload: Action = {type: action, payload};
		var propertyName: string = action.split(':')[0];

		var currentState: any = this.getState();
		var stateProperty = this.stateProperties.filter(p => p.propName === propertyName)[0];

		var newSubState = stateProperty.reducerFunc(currentState[propertyName], actionWithPayload);

		var newStatePropertyValue: any = {};
		newStatePropertyValue[propertyName] = newSubState;

		this.pushState(Object.assign({}, currentState, newStatePropertyValue));
		stateProperty.actions.push(actionWithPayload);
		stateProperty.subject.next({lastAction: actionWithPayload, currentValue: newStatePropertyValue[propertyName]} as StateChange<any>);
	}

	public select(propertyName: string): BehaviorSubject<StateChange<any>> {
		var stateProperty = this.stateProperties.filter(p => p.propName === propertyName)[0];
		return stateProperty.subject;
	}
}