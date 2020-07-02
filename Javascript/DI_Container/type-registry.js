var TypeRegistryLifecycle = {
	dynamic: "dynamic",
	static: "static"
}

class TypeRegistry {

	registry = [];

	constructor() {

	}
	
    registerType(key, ctor, lifecycle) {
        this.container[key] = { ctor, lifecycle };
	}

	// ctor: can be a prototype function, or a static object (Plain Old Js Object)
	// lifecycle: can be dynamic or static (kinda singleton)
	registerElement(key, ctor, lifecycle) {
		customElements.define(key, ctor);
        this.container[key] = { ctor, lifecycle };
	}
	
    resolve(className, params) {

		switch(this.container[className].type) {
			case TypeRegistryLifecycle.dynamic:
				return Reflect.construct(this.container[className].ctor, params);
			case TypeRegistryLifecycle.static:
				return this.container[className].ctor;

			default: return undefined;
		}		        	
	}
}

// add one instance to window.app but
// or can be done in the app index.js or something
window.app = window.app || {};
window.app.typeRegistry = new TypeRegistry();
