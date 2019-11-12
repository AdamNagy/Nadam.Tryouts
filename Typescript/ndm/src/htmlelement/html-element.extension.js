HTMLElement.prototype.WithInnerText = function(text) {

	this.innerText = text;
	return this;
}

HTMLElement.prototype.WithAttribute = function(key, value) {

	this.setAttribute(key, value);
	return this;
}

HTMLElement.prototype.WithStyle = function(key, value) {

	this.style[key] = value;
	return this;
}

HTMLElement.prototype.WithClass = function(name) {

	this.classList.add(name);
	return this;
}

HTMLElement.prototype.WithId = function(id) {
	this.setAttribute("id", id);
	return this;
}
