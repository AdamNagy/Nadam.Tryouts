import { BehaviorSubject } from "rxjs/BehaviorSubject";

export class ReducableProperty {
	public propName: string = "";
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
	public lastAction: Action | undefined;
	public currentValue: any;
}

export class ReduxStore {

	private stateQueue: any[] = [];
	private stateProperties: StateProperty[] = [];

	constructor(_reducables: ReducableProperty[]) {

		let initialState: any = {};
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

	public getState(): any {
		return this.stateQueue[this.stateQueue.length - 1];
	}

	public addProperty(reducable: ReducableProperty) {
		const newProp: any = {};
		newProp[reducable.propName] =  reducable.reducerFunc();

		const initialState = Object.assign({}, this.getState(), newProp);

		this.stateProperties.push({
			propName: reducable.propName,
			reducerFunc: reducable.reducerFunc,
			subject: new BehaviorSubject<StateChange<any>>({ lastAction: undefined, currentValue: newProp[reducable.propName] }),
			actions: [],
		});

		this.stateQueue.push(initialState);
	}

	public dispatch(action: string, payload: any) {

		const actionWithPayload: Action = {type: action, payload};
		const propertyName: string = action.split(":")[0];

		const currentState: any = this.getState();
		const stateProperty = this.stateProperties.find((p) => p.propName === propertyName);

		if( stateProperty === undefined )
			throw `reducer does not exist ${propertyName}`;
		
		const newSubState = stateProperty.reducerFunc(currentState[propertyName], actionWithPayload);

		const newStatePropertyValue: any = {};
		newStatePropertyValue[propertyName] = newSubState;

		this.stateQueue.push(Object.assign({}, currentState, newStatePropertyValue));
		stateProperty.actions.push(actionWithPayload);
		stateProperty.subject.next({lastAction: actionWithPayload, currentValue: newStatePropertyValue[propertyName]} as StateChange<any>);
	}

	public select(propertyName: string): BehaviorSubject<StateChange<any>> {
		const stateProperty = this.stateProperties.find((p) => p.propName === propertyName);

		if( stateProperty === undefined )
			throw `reducer does not exist ${propertyName}`;					

		return stateProperty.subject;
	}
}
