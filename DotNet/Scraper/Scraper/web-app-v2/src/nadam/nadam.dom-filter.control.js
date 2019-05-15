// type: Domain logic
// file name: nadam.dom-filter.js
// class name: DomFilter
// namespace: Nadam

/// <predicateSelector> is a dom selector, the returned elements will be passed to the predicate
/// <elementSelector> is a dom selector, the actual elements that the filter (hide or show) will apply
export class DomFilter {

	constructor (_predicateSelector, _elementSelector){

		this.predicateSelector = _predicateSelector;
		this.elementSelector = _elementSelector || _predicateSelector;
	}
	
	Filter(predicate) {
		var predicateElements = document.querySelectorAll(predicateSelector);
		var elements = document.querySelectorAll(elementSelector);

		for(var i = 0; i < elements.length; ++i) {
			if( predicate(predicateElements[i]) )
				continue;
			
			elements[i].style.display = "none";
		}
	};

	ResetFilter() {
		var elements = document.querySelectorAll(elementSelector);
		for(var i = 0; i < elements.length; ++i)	
			elements[i].style.display = "block";			
	};
}