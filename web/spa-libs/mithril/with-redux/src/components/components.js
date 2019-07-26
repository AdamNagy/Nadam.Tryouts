import m from 'mithril';
import {connect} from 'mithril-redux';
import {incrementAge, decrementAge, resetAge} from '../store/actions';

var JsxChildComponentFactory = {

	selector: (state) => ({name: state.name, age: state.age}),
	eventActionMapping: {
		inc: incrementAge,
	},
	view: function() {
		return (
			<jsx-child>
				<p>Hello from nested child cop</p>
			</jsx-child>
		)
	}
}

export const ChildComp = connect(
	JsxChildComponentFactory.selector,
	JsxChildComponentFactory.eventActionMapping
)(JsxChildComponentFactory);

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
				<jsx-child>
				</jsx-child>
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

