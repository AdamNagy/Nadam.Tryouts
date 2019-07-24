class Pubsub {

	subscriptions = new Array();

	subscribe = function(eventName, action) {
		this.subscriptions.push(action);
	}

	fire = function(eventName, payload) {
		for(var i = 0; i < this.subscriptions.length; ++i) {
			this.subscriptions[i](payload);
		}
	}
}

class Observable {

	subscriptions = new Array();
	value;

	subscribe = function(action) {
		this.subscriptions.push(action);
	}

	changeValue(newVal) {
		this.value = newVal;
		this.fire("changedValue", this.value);
	}

	fire = function(eventName, payload) {
		for(var i = 0; i < this.subscriptions.length; ++i) {
			this.subscriptions[i](payload);
		}
	}

	constructor(_value) {
		if( _value !== undefined )
			this.value = _value;
	}
}

var propSelector = function(selector) {

	return window.store[selector];
}

class Component {

	prop1 = window.store.prop1;
	CompName;

	constructor(_name) {

		this.CompName = _name;

		window.pubsub.subscribe("prop1", (payload) => {
			this.prop1 = payload;
			console.log({prop: this.prop1, name: this.CompName});
		});
	};
}

class ModernComponent {

	prop2;
	CompName;

	constructor(_name) {

		this.$prop2 = propSelector("prop2");
		this.CompName = _name;

		this.$prop2.subscribe((payload) => {
			this.prop2 = payload;
			console.log({prop: this.prop2, name: this.CompName});
		});
	};
}

window.store = window.store || {};
window.store.prop1 = {val: 1};
window.store.prop2 = new Observable();

window.pubsub = new Pubsub();
window.components = new Array();
// window.components.push(new Component("comp2"));
// window.components.push(new Component("comp1"));
// window.components.push(new Component("comp3"));

window.components.push(new ModernComponent("comp2"));
window.components.push(new ModernComponent("comp1"));
window.components.push(new ModernComponent("comp3"));
// window.components.push(new ModernComponent("comp1"));
// window.components.push(new ModernComponent("comp3"));