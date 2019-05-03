"use strict";

/// <predicateSelector> is a dom selector, the returned elements will be passed to the predicate
/// <elementSelector> is a dom selector, the actual elements that the filter (hide or show) will apply
function DomFilter(predicateSelector, elementSelector) {

	// <private_properties>
	var _predicateSelector = predicateSelector;
	var _elementSelector = elementSelector || predicateSelector;
	// </private_properties>
	
	// <public_properties>
	this.Filter = function(predicate) {
		var predicateElements = document.querySelectorAll(_predicateSelector);
		var elements = document.querySelectorAll(_elementSelector);

		for(var i = 0; i < elements.length; ++i) {
			if( predicate(predicateElements[i]) )
				continue;
			
			elements[i].style.display = "none";
		}
	};

	this.ResetFilter = function () {
		var elements = document.querySelectorAll(_elementSelector);
		for(var i = 0; i < elements.length; ++i)	
			elements[i].style.display = "block";			
	};
	// </public_properties>
}