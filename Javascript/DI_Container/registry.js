// can register default configs like 'DefaultAccordionConfig' to be global and handled by this registry
//		by resgistering static object you HTML Elements be saved as well
// can regsiter prototype functions (ctor) to be able to instantiate whenever needed like a DI container

var RegistryItemLifecycle = {
	dynamic: "dynamic",
	static: "static"
}

class Registry {

	registry = [];

	constructor() {

	}

	exist(key) {
		for(var _key in this.registry ) {
			if( key === _key ) {
				return true;
			}
		}

		return false;
	}

	// key: string value to have a unique identifier
	// ctor: can be a prototype function, or a static object (Plain Old Js Object)
	// lifecycle: can be dynamic or static (kinda singleton) defaults to dynamic
    register(key, ctor, lifecycle) {

		if(lifecycle === undefined)
			lifecycle = RegistryItemLifecycle.dynamic;

        this.registry[key] = { ctor, lifecycle };
	}

	// lifecycle: will be dynamic always
	registerElement(key, ctor) {

		customElements.define(key, ctor);
        this.registry[key] = { ctor, lifecycle: RegistryItemLifecycle.dynamic };
	}
	
    resolve(key, params) {

		if(!this.exist(key)) {
			return undefined;
		}

		switch(this.registry[key].lifecycle) {
			case RegistryItemLifecycle.dynamic:
				return Reflect.construct(this.registry[key].ctor, params);

			case RegistryItemLifecycle.static:
				return this.registry[key].ctor;

			default: return undefined;
		}		        	
	}
}
