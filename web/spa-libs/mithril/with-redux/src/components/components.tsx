import { connect } from "mithril-redux";
import { incrementAge } from "../store/actions";
import { MithrilComponent } from "./mithril.component";

// <Conmponent-1>
class ChildComponentFactory implements MithrilComponent {

	public static eventActionMapping = {
		// its because this 'mithril-redux' shit wires up all events in the markeup as a store action,
		// and need to use the thunk middleware
		dispatchNewAge: () => () => {
			(window as any).store.dispatch({type: "INCREMENT_AGE"} );
		},
	};
	public static selector(state) {return {name: state.name, age: state.age}};

	public static getComponent() {
		return {
			view: (ctrl, stateProjection, children) => { 
				return (
					<jsx-child>
						<h3>Hello from child component: Child component</h3>
						<p>html attributes: {stateProjection["some-attribute"]}</p>
						<input type="text" id="qwe"></input>
						<button onclick={ctrl.dispatchNewAge()}>Increment age</button>
					</jsx-child>
				)
			}
		}
	}
}

export const ChildComp = connect(
	ChildComponentFactory.selector,
	ChildComponentFactory.eventActionMapping
)(ChildComponentFactory.getComponent());
// </Conmponent-1>

// <Conmponent-2>
class BaseComponentFactory implements MithrilComponent {

	public static eventActionMapping = {
		inc: incrementAge,
	};

	public static selector(state) {return {name: state.name, age: state.age}};

	public static getComponent() {
		return {
			view: (ctrl, stateProjection, children) => {
				return (
					<jsx-person>
						<h2>Hello from Jsx compoent: Base component</h2>
						<p>from function parameter {stateProjection.name} age: {stateProjection.age}</p>
						<button onclick={ctrl.inc()}>Increment age</button>
						<hr></hr>
						<ChildComp some-attribute="123">
						</ChildComp>
					</jsx-person>
				)
			}
		}
	}
}

export const BaseComp = connect(
	BaseComponentFactory.selector,
	BaseComponentFactory.eventActionMapping
)(BaseComponentFactory.getComponent());
// </Conmponent-2>