import { fromEvent, merge, of } from "rxjs";
import "rxjs/add/operator/map";
import "rxjs/add/operator/merge";
import "rxjs/add/operator/scan";
import { Subject } from "rxjs/Subject";

interface IAppState {
	count: number;
	name: string;
	render: string;
	comp: number;
}

const appState: IAppState = {
	comp: 1,
	count: 0,
	name: "",
	render: "",
};

const stateSubject = new Subject<IAppState>();

/**************************************************************************/
// component 1
export class MyDom {

	public componentDom: any = {};
	public observables: any = {};

	protected ROOT = "root";

	constructor(element: HTMLElement) {

		this.componentDom[this.ROOT] = element;
		this.Scan(this.componentDom[this.ROOT]);
	}

	private Scan(root: HTMLElement) {
		for (const item of root.children) {
			const id = item.getAttribute("id");
			if ( id === undefined )
				continue;

			this.componentDom[id] = item;

			if ( item.nodeName.toLowerCase() === "button" ) {
				this.observables[id] = fromEvent(item, "click")
					.map(() => (state: IAppState) => {
						// this is reducer part, here define what should be changed in the state
						return Object.assign(appState, { comp: state.comp + 3, render: "comp" });
					})
					.scan((state: IAppState, changeFn: any) => changeFn(state), appState);
			}

			if ( item.nodeName.toLowerCase() === "input" )
				this.observables[id] = fromEvent(item, "input");

			this.Scan(item as HTMLElement);
		}
	}
}

const main = document.getElementById("main");
export const mainDom = new MyDom(main);
mainDom.observables["my-btn"]
	// this is the rendering part
	.subscribe();

/**************************************************************************/
// component 2

// dom element references to be able reflect back the state to UI
const labelCount = document.getElementById("label-count");
const labelName = document.getElementById("label-name");

// action elements: button dom element reference
const increaseButton = document.querySelector("#increment");
// the button click event observable
const increase = fromEvent(increaseButton, "click")
	// Again we map to a function the will increase the count: the action
	.map(() => (state: IAppState) => Object.assign(appState, { count: state.count + 1, render: "count" }));

const decreaseButton = document.querySelector("#decrease");
const decrease = fromEvent(decreaseButton, "click")
	.map(() => (state: IAppState) => Object.assign(appState, { count: state.count - 1, render: "count" }));

const inputElement = document.querySelector("#input-name");
const input = fromEvent(inputElement, "input")
	// Let us also map the keypress events to produce an inputValue state
	// this map transfor the source observable to a function required for the 'scan' below
	.map((event: any) => (state: IAppState) => Object.assign(appState, { name: event.target.value,  render: "name" }));

// We merge the three state change producing observables
const store = merge(
	increase,
	decrease,
	input,
// scan is like reduce in lodash or any aggregation function in SQL: given a sequence and generates a single value
// in case of stream the 'single value' is a steam as well.
// the function required for scan is coming from 'map' operator of the merged observables above
).scan((state: IAppState, changeFn: any) => changeFn(state), appState);

// rendering and the engine of the above all
store.subscribe((state: IAppState) => {
	// appState = state;
	switch (appState.render) {
		case "count": labelCount.innerHTML = appState.count.toString();
			break;
		case "name": labelName.innerHTML = "Hello " + appState.name;
			break;
	}
	stateSubject.next(appState);
	console.log({appState});
});

// its like effect for a certein event (or part of the state that has changed to a certein event)
// this will be in separated components, and all will listen to relevant part
stateSubject.subscribe((state: IAppState) => {

	switch (appState.render) {
		case "count": if ( state.count === 10 ) alert("hello 10");
			break;
		case "name": if (state.name === "adam") alert("hello adam");
			break;
	}
});

/**************************************************************************/
// some other tryout
document.body.append(document.createElement("hr"));
const primes = document.createElement("div");
document.body.append(primes);

const sourceObservable = of([1, 2, 3, 5, 7, 11]);
const primSubject = new Subject<number[]>();

primSubject.subscribe((p) => {
	primes.append(p.toString());
	console.log(p);
});

sourceObservable.subscribe(primSubject);
