// import { Subject } from "rx";
import { fromEvent } from "rxjs";
import { Observable, Subject } from "rxjs/Rx";

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

/***************************/

// component 2

// dom element references to be able reflect back the state to UI
const labelCount = document.getElementById("label-count");
const labelName = document.getElementById("label-name");

// action elements: button dom element reference
const increaseButton = document.querySelector("#increment");
// the button click event observable
const increase = Observable.fromEvent(increaseButton, "click")
	// Again we map to a function the will increase the count: the action
	.map(() => (state: IAppState) => Object.assign(appState, { count: state.count + 1, render: "count" }));

const decreaseButton = document.querySelector("#decrease");
const decrease = Observable.fromEvent(decreaseButton, "click")
	.map(() => (state: IAppState) => Object.assign(appState, { count: state.count - 1, render: "count" }));

const inputElement = document.querySelector("#input-name");
const input = Observable.fromEvent(inputElement, "input")
	// Let us also map the keypress events to produce an inputValue state
	.map((event: any) => (state: IAppState) => Object.assign(appState, { name: event.target.value,  render: "name" }));

// We merge the three state change producing observables
const store = Observable.merge(
	increase,
	 decrease,
 	 input,
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
