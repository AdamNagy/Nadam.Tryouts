// this will return a function which able to update the 'value' with given parameter
// or just simply returns it
// val: the initial value of the property (string, number, object, array)
// boundElement: and HTMLElement which will be updated when value changes
// attrName: this is the name of the attribute of the previous HTMLElement that will hold the value as string
//   default is 'innerText'
var Property = function(val, boundElement, attrName) {
	var _val = val;

	if( boundElement !== undefined ) {
		var update = Binding(boundElement, attrName);
		update(_val);
	}

	// the new value if the property if given, so acts as a setter
	// if undefined the fuction acts as a getter
	return function (val, operation) {
		
		if( val === undefined ) {
			if( typeof _val === "function" )
			return _val();
			return _val;
		}
		
		operation = operation === undefined ? "assignment" : operation;
		switch(operation) {
			case "assignment": _val = val;
				break;
			case "add": _val.push(val);
				break;
			case "remove": 
				var idx = _val.indexOf(val);
				if( idx > -1 ) _val.splice(idx, 1);
				break;
		}

		
		if( update !== undefined ) {
			if( typeof _val === "function" ) {
				update(_val());
				return _val();
			}
			else {
				update(_val);
				return _val; 
			}
		}
	}			
}

const SimpleProperty = function(val, boundElement, attrName) {

	var _val = val;

	if( boundElement !== undefined ) {
		var update = Binding(boundElement, attrName);
		update(_val);
	}

	this.get = function(val) {		
		return _val;		
	};

	this.set = function(newValue) {
		_val = newValue;
		if( update !== undefined )
			update(_val);
		return _val; 
	}
}

const ArrayProperty = (initialValue, parentElement, projectorFunc) => {

	var _value = initialValue;
	var parent = parent || undefined;
	
	this.get = function() {

	};

	this.set = function() {

	}

	this.add = function() {

	}

	this.clear = function() {

	}

	this.remove = function() {

	}
}

// this returns a function as well, calling it will update the DOM wit the given values
// element: the HTMLElement to bound value to
// attrName: the attribute name to put the value into
var Binding = function(element, attrName) {

	var _element = element;
	var _attrName = attrName === undefined ? "innerText" : attrName;	// default attribute is innerText

	// the value to update the DOM HTMLElement with
	return function (newValue) {
		
		// <_attrname> is string like 'innerTEXT' or 'id' or 'propsToAttrs' which is special
		if ( typeof _attrName === "string") {

			// special case <newValue> is an object and its properties needs to be mapped to <_element> attributes
			if ( _attrName === "propsToAttrs" && IsObject(newValue) ) {
			
				for(var propName in newValue) {
					_element.setAttribute(CamelCaseToDashed(propName), newValue[propName]);
				}

			// <_attrMame> in this case is a HTMLElement attribute name like 'innerText' or 'id', 'class' etc..
			// <_element> is array multiple HTMLElement need to be updated
			} else if ( IsArray(_element) &&  IsObject(newValue)) {

				var valAsArray = ObjectToArray(newValue);
				for(var i = 0; i < _element.length; ++i) {
					_element[i][_attrName] = valAsArray[i];
				}

			// basic case:
			// <_attrMame> is a attribute name
			// <_element> is a single HTMLElement
			// <newValue> is string
			} else {
				// there is difference between property and attribute.
				// _element[_attrName] = newValue; will work only if _attrName is already exist on HTMLElement
				if ( IsHTMLElementProperty(_attrName) )
					_element[_attrName] = newValue;
				else
					_element.setAttribute(CamelCaseToDashed(_attrName), newValue);
			}
		}
		// <_attrName> is a function that generates HTMLElement (children) and will be append to <_element> (parent)
		else if(typeof _attrName === "function") {

			// purge the parent element inside to make sure no duplicity takes place
			_element.innerHTML = "";

			// 2 cases here:
			// A: <newValue> is any array of strings
			if ( IsArray(newValue) ) {
				for(var i = 0; i < newValue.length; ++i) {
					_element.append(
						_attrName(newValue[i])
					)
				}
			// B: <newValue> is string
			} else {
				_element.append(_attrName(newValue));
			}
		}
	}
}

// this could go to html-element.extension but logically belongs to this file
HTMLElement.prototype.BindValue = function(event, action) {
	this.addEventListener(event, Throttle(action, 400));
	return this;
}
