import m from 'mithril';
import { connect } from 'mithril-redux';
import { incrementAge } from '../store/actions';

// <Conmponent-1>
class ChildComponentFactory {
	
	constructor(stringAttribute) {
		this.attr = stringAttribute;
		this.eventActionMapping = {
			// its because this 'mithril-redux' shit wires up all events in the markeup as a store action,
			// and need to use the thunk middleware
			dispatchNewAge: () => () => {
				window.store.dispatch({type: 'INCREMENT_AGE'})
			}
		}
	}

	selector(state) {return {name: state.name, age: state.age}}

	getComponent() {
		let someProp = "somewhat class property";
		let classProp = this.attr;

		return {
			view: function(ctrl, stateProjection, children) { 
				return (
					<jsx-child>
						<h3>Hello from child component: Child component</h3>
						<p>static class property: {someProp}</p>
						<p>class dependecy: {classProp}</p>
						<p>html attributes: {stateProjection["some-attribute"]}</p>
						<input type="text" id="qwe"></input>
						<button onclick={ctrl.dispatchNewAge()}>Increment age</button>
					</jsx-child>
				)
			}
		}
	}
}

var comp = new ChildComponentFactory("some given ctor attribute");
export const ChildComp = connect(
	comp.selector,
	comp.eventActionMapping
)(comp.getComponent());
// </Conmponent-1>

// <Conmponent-2>
var BaseComponentFactory = {

	selector: (state) => ({name: state.name, age: state.age}),
	eventActionMapping: {
		inc: incrementAge,
	},

	view: function(ctrl, stateProjection) {
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

export const BaseComp = connect(
	BaseComponentFactory.selector,
	BaseComponentFactory.eventActionMapping
)(BaseComponentFactory);
// </Conmponent-2>