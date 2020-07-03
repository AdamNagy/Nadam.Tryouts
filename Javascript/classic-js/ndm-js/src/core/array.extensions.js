Array.prototype.First = function(predicate) {

	if (predicate == null || typeof predicate !== 'function') {
		throw "Predicate is not a function!";
	}

	for (var i = 0; i < this.length; ++i) {
		if (predicate(this[i])) {
			return this[i];
		}
	}

	return undefined;
}

Array.prototype.Last = function(predicate) {

	if (predicate == null || typeof predicate !== 'function') {
		throw "Predicate is not a function!";
	}
	
	var filtered = this.filter(predicate);
	
	if( !filtered || filtered.length == 0 )
		return undefined;
	
	return filtered[filtered.length - 1];
}

Array.prototype.Skip = function(amount) {

	return this.splice(amount, this.length-1);
}

Array.prototype.Take = function(amount) {

	return this.splice(0, amount);
}

Array.prototype.Select = function(action) {

	if (action == null || typeof action !== 'function') {
		throw "Action is not a function!";
	}
	
	var list = new Array();

	for (var i = 0; i < this.length; ++i) {
		list.push(action(this[i]));
	}

	return list;
}
