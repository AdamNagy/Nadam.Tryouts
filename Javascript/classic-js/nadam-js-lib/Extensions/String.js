String.prototype.ReplaceAll = function(search, replacement) {
	var target = this;
	return target.split(search).join(replacement);
};
