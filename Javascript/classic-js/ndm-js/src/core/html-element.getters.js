HTMLElement.prototype.Value = function() {
	if( this.value !== undefined )
		return this.value;
	else
		return this.innerText;
}

HTMLElement.prototype.Height = function() {
	return this.height;
}

HTMLElement.prototype.Width = function() {
	return this.width;
}