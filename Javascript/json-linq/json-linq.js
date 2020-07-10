function isPrimitive(variable) {
	var type = typeof variable;

	return type === "number" || type === "boolean" || type === "string";
}

function isObject(variable) {
	var type = typeof variable;

	return type === "object" && !Array.isArray(variable);
}

function isIndexedArray(someArray) {
	if( !Array.isArray(someArray) )
		return false;

	var idx = 0;
	for(var i in someArray) {
		if( i !== idx )
			return false;
		
		++idx;
	}

	return true;
}

class JsonLinq {

	_jsonString = "";
	_jsonObj = {};

	constructor(jsonString) {
		this._jsonString = jsonString;
		try {
			this._jsonObj = JSON.parse(jsonString);
		} catch {
			console.log("shit hapens");
		}
	}

	select(propName) {
		return this.searchForProp(propName, this._jsonObj);
	}
	
	/* static */ 
	searchForProp(propName, jsonObj) {
		
		var found = {}; 

		// primitive: number, string, boolean
		if( isPrimitive(jsonObj) ) {
			return undefined;

		}
		// array
		else if( Array.isArray(jsonObj) && jsonObj.length > 0 ) {
			return this.searchForProp(propName, jsonObj[0])
		}
		// object
		else if( isObject(jsonObj) ) {

			for(var prop in jsonObj) {
				if( prop === propName ) {
					return jsonObj[prop];
				}
			}
	
			// go deeper
			for(var prop in jsonObj) {
	
				found = this.searchForProp(propName, jsonObj[prop]);
				if( found ) {
					return found;
				}
			}
		}
		else {
			return undefined;
		}
	}
}