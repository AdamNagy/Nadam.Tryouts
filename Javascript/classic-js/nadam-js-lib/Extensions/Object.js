Object.prototype.HasProperty = function(name) {
	return this[name] !== undefined;
}

// inpure function
Object.prototype.AddProperty = function(name, value) {
	if (!this.HasProperty(name)) {
		this[name] = value;
	}
	return this;
}