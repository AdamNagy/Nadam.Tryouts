;
"use strict";

/// <summary>
/// 
/// </summary>
/// <returns></returns>
var GetById = function(id) {
	var sameIds = document.getElementById(id);

	if(sameIds.length !== undefined && sameIds.length >= 1) {
		sameIds[0];
	}

	return sameIds;
}

/// <summary>
/// 
/// </summary>
/// <returns></returns>
var GetByClass = function(className) {
    return document.getElementsByClassName(className);
}

var GetByTagName = function(tagName) {
    return document.getElementsByTagName(tagName);
}

var IsPrimitive = function(variable) {
	
    var type = typeof variable;

    if (type === "number" || type === "string" || type === "boolean") {
        return true;
    }

    return false;
}

var IsUndefined = function(variable) {

    return typeof variable === "undefined"
}

var IsFunction = function(variable) {

    return typeof variable === "function";
}

var IsObject = function(variable) {

    return typeof variable === "object";
}

var RemoveScripts = function() {

	var script = GetByTagName(HtmlTags.script)[0];
	while(script !== null){
		script.remove();
	}
}

var RemoveIFrames = function() {

	var iframe = GetByTagName(HtmlTags.iframe)[0];
	while(iframe !== null){
		iframe.remove();
	}
}