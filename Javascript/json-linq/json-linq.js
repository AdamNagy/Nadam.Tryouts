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

function trimChar(string, charToRemove) {
    while(string.charAt(0)==charToRemove) {
        string = string.substring(1);
    }

    while(string.charAt(string.length-1)==charToRemove) {
        string = string.substring(0,string.length-1);
    }

    return string;
}

class JsonLinq {
	
	static select(propName, jsonObj, recursive) {

		if( jsonObj === null || jsonObj === undefined)
			return undefined;

		if( recursive === undefined ) {
			recursive = true;
		}

		var found = {}; 

		// primitive: number, string, boolean
		if( isPrimitive(jsonObj) ) {
			return undefined;
		}
		// array
		else if( Array.isArray(jsonObj) && jsonObj.length > 0 ) {
			var selecResults = [];
			var selecResult = JsonLinq.select(propName, jsonObj[0]);
			if( selecResult ) {
				selecResults.push(selecResult);
				for(var arrayItem of jsonObj.slice(1)) {
					selecResults.push(JsonLinq.select(propName, arrayItem));
				}

				return selecResults;
			}
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
	
				if( recursive )
					found = JsonLinq.select(propName, jsonObj[prop]);

				if( found ) {
					return found;
				}
			}
		}
		else {
			return undefined;
		}
	}

	static where(propName, pred, jsonArray) {

		if( !Array.isArray(jsonArray) || jsonArray.length < 1 )
			return undefined;

		return jsonArray.filter(item => item[propName] === pred);
	}

	// example
	// select:markets; where:eventId=f-ED0cC15OhkORmVTK2V2txg
	static query(query, jsonObj) {

		if( query === undefined || query === ""
			|| jsonObj === null || jsonObj === undefined )
			return undefined;

		query = trimChar(query.trim(), ";");
		var instructions = query.split(';');

		var resultState = [];
		resultState.push(jsonObj);

		for( var instruction of instructions ) {

			var parsedInstruction = instruction.split(':');
			if( parsedInstruction.length < 2 )
				throw `Invalid instruction: ${instruction}`;

			var queryCommand = parsedInstruction[0].trim();
			var commandParameter = parsedInstruction[1].trim();

			switch( queryCommand ) {
				case "select":
					var queryState = resultState[resultState.length - 1];
					if( !queryState )
						throw "query state is undefined";
						
					var result = JsonLinq.select(commandParameter, queryState);

					if( result !== undefined ) {
						resultState.push(result);
					}

					continue;

				case "where":
					var queryState = resultState[resultState.length - 1];

					if( !queryState )
						throw "query state is undefined";

					if( !Array.isArray(queryState) )
						throw `json object is not an array, but query command is "where" which require an array`

					var whereParams = commandParameter.split('=').map(item => item.trim()).map(item => trimChar(item, "\"")).map(item => trimChar(item, "\'"));
					var result = JsonLinq.where(whereParams[0], whereParams[1], queryState);
					if( result !== undefined ) {
						resultState.push(result);
					}

					continue;

				default: throw `Unkown query command: ${queryCommand}`;
			}
		}

		return resultState[resultState.length - 1]
	}
}
