// attribute
HTMLElement.prototype.WithAttribute = function(key, value) {
	this.setAttribute(key, value);
	return this;
}

HTMLElement.prototype.WithoutAttribute = function(key, value) {
	this.removeAttribute(key);
	return this;
}

// style
HTMLElement.prototype.WithStyle = function(key, value) {
	this.style[key] = value;
	return this;
}

HTMLElement.prototype.WithCss = function(cssObj) {
	for(var propName in cssObj) {
		this.style[propName] = cssObj[propName];
	}
	return this;
}

// class
HTMLElement.prototype.WithClass = function(name) {
	this.classList.add(name);
	return this;
}


HTMLElement.prototype.WithClasses = function(classNames) {

	classNames.forEach(
		item => item.split(" ").forEach(
			className => this.classList.add(className)
		)
	);
	
	return this;
}

HTMLElement.prototype.WithoutClass = function(className) {

	if(this.classList.contains(className))
		this.classList.remove(className);
	
	return this;
}

// inner text/value
HTMLElement.prototype.WithInnerText = function(text) {
	this.innerText = text;
	return this;
}

HTMLInputElement.prototype.WithValue = function(val) {
	if( this.value !== undefined )
		this.value = val;
	else
		this.innerText = val;

	return this;
}

// id
HTMLElement.prototype.WithId = function(id) {
	this.setAttribute("id", id);
	return this;
}

// child element
HTMLElement.prototype.WithChild = function(child){
	this.append(child);
	return this;
}

HTMLElement.prototype.WithChildren = function(children) {
	for(var i = 0; i < children.length; ++i) {
		this.append(children[i]);
	}
	return this;
}

HTMLElement.prototype.WithoutChildren = function() {

	var children = this.children;
	while (children !== undefined && children.length > 0) {
		for (var i = 0; i < children.length; ++i) {
			this.children[i].remove();
		}
		children = this.children;
	}

	return this;
}

// event listener
HTMLElement.prototype.WithEventListener = function(event, func) {
	this.addEventListener(event, func);
	return this;
}

HTMLElement.prototype.WithoutEventListener = function(event) {
	this.removeEventListener(event);
	return this;
}

HTMLElement.prototype.WithOnClick = function(func) {
	this.addEventListener("click", func);
	return this;
}

HTMLElement.prototype.WithOnLoad = function(func) {
	this.addEventListener("load", func);
	return this;
}

// other
HTMLElement.prototype.ToParent = function(parent) {
	parent.append(this);
	return this;
}