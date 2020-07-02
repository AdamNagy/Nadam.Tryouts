// in this context array is not an object
var IsObject = function(variable) {
	return( typeof variable === "object" && variable.length === undefined )
}

var IsArray = function(variable) {
	return( typeof variable === "object" && variable.length !== undefined )
}

// convert an object to an array using the values only
var ObjectToArray = function(obj) {
	var arr = [];
	for(var propname in obj) { 
		arr.push(obj[propname]); 
	}

	return arr;
}

var CamelCaseToDashed = function(word) {
	var dashCased = word[0].toLowerCase();

	for(var i = 1; i < word.length; ++i) {
		if( word[i] === word[i].toUpperCase() && word[i] !== "-")
			dashCased += "-";
		 
		dashCased += word[i].toLowerCase();
	}
	
	// trim '-' from start
	while( dashCased[0] === '-' ) {
		dashCased = dashCased.slice(1);
	}

	// trim '-' from end
	while( dashCased[dashCased.length - 1] === '-' ) {
		dashCased = dashCased.slice(0, dashCased.length - 1);
	}

	// replaces all multiple dasheses next to each other with one dash only
	return dashCased.replace(new RegExp('[-]+', 'g'), '-');
}

var IsHTMLElementProperty = function(word) {
	// need to list all HTMLElement property
	if( word === "innerText" )
		return true;

	return false;
}

var Throttle = function(func, limit) {
	var lastFunc;
	var lastRan;

	return function() {
		var context = this;
		var args = arguments;

		if ( !lastRan ) {
			func.apply(context, args);
			lastRan = Date.now();
		} else {
			clearTimeout(lastFunc);
			lastFunc = setTimeout(function() {
				if ((Date.now() - lastRan) >= limit) {
					func.apply(context, args)
					lastRan = Date.now()
				}
			}, limit - (Date.now() - lastRan))
		}
	}
}

var IsElement = function(element) {
    return element instanceof Element || element instanceof HTMLDocument;  
}
