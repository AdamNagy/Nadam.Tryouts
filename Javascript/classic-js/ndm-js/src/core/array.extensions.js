Array.prototype.First = function(predicate) {

	if (predicate == null || typeof predicate !== 'function') {
		throw "Predicate is not a function!";
	}

	var found;

	for (var i = 0; i < this.length; ++i) {
		if (predicate(this[i])) {
			found = this[i];
		}
	}

	return found;
}

Array.prototype.Last = function(predicate) {

	if (predicate !== null && predicate !== undefined) {
		var filtered = this.filter(predicate);
		return filtered[filtered.length - 1];
	}

	return this[this.length - 1];
}

Array.prototype.Skip = function(amount) {

	return this.splice(amount, this.length-1);
}

Array.prototype.Take = function(amount) {

	return this.splice(0, amount);
}

Array.prototype.Where = function(predicate) {

	return this.filter(predicate);
}

Array.prototype.Select = function(action) {

	var list = new Array();
	if (action == null || typeof action !== 'function') {
		throw "Action is not a function!";
	}

	for (var i = 0; i < this.length; ++i) {
		list.push(action(this[i]));
	}

	return list;
}
