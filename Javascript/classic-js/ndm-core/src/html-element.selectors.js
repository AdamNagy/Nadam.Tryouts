HTMLElement.prototype.$nid = function(nid) {
	if (nid === "")
		return this;
	return this.querySelector('[nid="' + nid + '"]');
}

HTMLElement.prototype.$class = function(selector) {
	if (selector === "")
		return this;
	return this.querySelector('[class*="' + selector + '"]');
}

HTMLElement.prototype.$ = function(selector) {
	if (nid === "")
		return this;
	return this.querySelector(selector);
}


/// <summary> 
/// Iterates throught the direct childrend of the node, and flattens it into a list
/// Optionally filters the list
/// </summary>
Element.prototype.AllChildren = function(predicate) {

	var firstChild = this.firstElementChild;
	var directchildren = new Array();

	if (firstChild !== null) {
		directchildren.push(firstChild);
	} else {
		return;
	}

	var sibling = firstChild.nextElementSibling;
	while (sibling != null) {
		directchildren.push(sibling);
		sibling = sibling.nextElementSibling;
	}

	if(predicate !== null && predicate !== undefined ) {
		return directchildren.filter(predicate);
	}

	return directchildren;
}

HTMLElement.prototype.ToParent = function(parent) {
	parent.append(this);
	return this;
}
