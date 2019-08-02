function Dictionary() {
	this.add = add;
	this.datastore = new Array();
	this.find = find;
	this.remove = remove;
	this.showAll = showAll;
	}

function add(key, value) {
	this.datastore[key] = value;
}

function find(key) {
	return this.datastore[key];
}

function remove(key) {
	delete this.datastore[key];
}

function count() {
	return datastore.count;
}

function clear() {
	for (var i = 0; i < count; ++i) {
		delete this.datastore[i];
	}
}