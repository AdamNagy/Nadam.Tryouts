import m from 'mithril';
import { connect } from 'mithril-redux';
import {incrementAge, decrementAge, resetAge} from '../store/actions';

class JsxChildComponentFactory {

	constructor(attribute) {
		this.attr = attribute;
		this.eventActionMapping = {
			inc: incrementAge,
		};
		
	}
	
	getProp() {return this.someProp}
	selector(state) {return {name: state.name, age: state.age}}
	
	getComponent() {
		let someProp = "somewhat class property";
		let classProp = this.attr;

		return {
			view: function(ctrl, stateProjection, attributes) { return (
				<jsx-child>
					<h3>class property: {someProp}</h3>
					<h2>this is class dependecy: {classProp}</h2>
					<p>Hello from nested child cop</p>
					<p>attributes: {attributes}</p>
				</jsx-child>
			)}
		}
	}
}

var comp = new JsxChildComponentFactory("some given ctor attribute");
export const ChildComp = connect(
	comp.selector,
	comp.eventActionMapping
)(comp.getComponent());

var JsxComponentFactory = {

	selector: (state) => ({name: state.name, age: state.age}),
	eventActionMapping: {
		inc: incrementAge,
	},

	view: function(ctrl, stateProjection) {
		return (
			<jsx-person>
				Hello from Jsx compoent
				<p>from function parameter {stateProjection.name} age: {stateProjection.age}</p>
				<button onclick={ctrl.inc()}>Increment something</button>
				<ChildComp some-attribute={stateProjection.age}>
				</ChildComp>
			</jsx-person>
		)
	}
}

export const NameBoxJsx = connect(
	JsxComponentFactory.selector,
	JsxComponentFactory.eventActionMapping
)(JsxComponentFactory);


class _NameBox {
  view(ctrl, {name}) {
    return m('div', 'Hello ' + name);
  }
}

class _AgeBox {
  view(ctrl, {age}) {
    return m('div', [
        m('span', 'Age: ' + age),
        m('button', {onclick: ctrl.dec()}, 'Younger'),
        m('button', {onclick: ctrl.inc()}, 'Older'),
        m('button', {onclick: ctrl.reset()}, 'Reset')
    ]);
  }
}

export const NameBox = connect(
	(state) => ({name: state.name})
)(_NameBox);


export const AgeBox = connect(
	(state) => ({age: state.age}), // selector function
	{	// controller, mapper from events (click) to store actions 
		inc: incrementAge,
		dec: decrementAge,
		reset: resetAge
	}
)(_AgeBox);

