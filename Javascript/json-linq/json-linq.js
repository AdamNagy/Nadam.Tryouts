function isPrimitive(variable) {
	var type = typeof variable;

	return type === "number" || type === "boolean" || type === "string";
}

function isJsonObject(variable) {
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

	while(string.charAt(0) == charToRemove) {
		string = string.substring(1);
	}

	while(string.charAt(string.length - 1) == charToRemove) {
		string = string.substring(0, string.length - 1);
	}

	return string;
}

class JsonLinq {
	
	static select(propName, jsonObj) {

		if( jsonObj === null || jsonObj === undefined) {
			return undefined;
		}

		if( propName === undefined || propName === null ) {
			return undefined;
		}

		var found; 

		// if property name contains dot (.) then it means it has to be solitted and search then one-by-one
		// example select: prop1.prop2.prop3 -> prop3 has to be contained by prop2, and prop2 has to be contained by prop1
		var propNames = propName.split('.');
		if( propNames.length > 1 ) {

			var propIdx = 0;
			var fatherProp = JsonLinq.select(propNames[propIdx], jsonObj);
			while( fatherProp !== undefined ) {
				++propIdx;
				fatherProp = JsonLinq.select(propNames[propIdx], fatherProp);

				found = fatherProp || found;
			}

			return found;
		}	

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
		else if( isJsonObject(jsonObj) ) {

			for(var prop in jsonObj) {
				if( prop === propName ) {
					return jsonObj[prop];
				}
			}
	
			// go deeper
			for(var prop in jsonObj) {	
				
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

	// example: project: {}
	static project(projectionRule, jsonObj) {
		
		var projection;
		var projectorFunc = JsonLinq.createProjector(projectionRule);

		if( isJsonObject(jsonObj) ) {

			projection = projectorFunc(jsonObj);

		} else if( Array.isArray(jsonObj) && jsonObj.length > 0) {

			projection = [];

			for(var item of jsonObj) {
				projection.push(projectorFunc(item));
			}
		}

		return projection;
	}

	// example: select:markets; where:eventId=f-ED0cC15OhkORmVTK2V2txg; project: {}
	static query(query, jsonObj) {

		if( query === undefined || query === ""
			|| jsonObj === null || jsonObj === undefined ) 
		{
			return undefined;
		}

		query = trimChar(query.trim(), ";");
		var instructions = query.split(';');

		var resultState = [];
		resultState.push(jsonObj);

		for( var instruction of instructions ) {

			var parsedInstruction = instruction.split(':');
			if( parsedInstruction.length < 2 ) {
				throw `Invalid instruction: ${instruction}`;
			}

			var queryCommand = parsedInstruction[0].trim();
			var commandParameter = parsedInstruction[1].trim();

			var querySubject = resultState[resultState.length - 1];
			if( !querySubject )
				throw "query subject is undefined";

			switch( queryCommand ) {
				case "select":						
					var result = JsonLinq.select(commandParameter, querySubject);

					if( result !== undefined ) {
						resultState.push(result);
					}

					continue;

				case "where":
					if( !Array.isArray(querySubject) )
						throw `json object is not an array, query command "where" require an array`

					var whereParams = commandParameter.split('=').map(item => item.trim()).map(item => trimChar(item, "\"")).map(item => trimChar(item, "\'"));
					var result = JsonLinq.where(whereParams[0], whereParams[1], querySubject);
					if( result !== undefined ) {
						resultState.push(result);
					}

					continue;

				case "project":
					var result = JsonLinq.project(commandParameter, querySubject);
					if( result !== undefined ) {
						resultState.push(result);
					}
					
					continue;

				default: throw `Unkown query command: ${queryCommand}`;
			}
		}

		return resultState[resultState.length - 1]
	}

	static createProjector(params) {

		var domain;

		try{
			// "{ newProp1: propertyOfTheInputItem-1, newProp2: propertyOfTheInputItem-2, ... }"
			// or
			// // "[ propertyOfTheInputItem-1, propertyOfTheInputItem-2, ... ]"
			domain = JSON.parse( params );

			return function(item) {

				var projection = {};
	
				if(Array.isArray(domain)) {

					var propertyIndex = 0;
					for( var prop of domain ) {
						projection[`p${++propertyIndex}`] = item[prop];
					}

				} else {

					for( var prop in domain ) {
						projection[prop]= item[domain[prop]];
					}

				}
	
				return projection;
			}
		} catch {
			throw `Could not generate projector function based on the given data: ${params}`;
		}
	}
}
