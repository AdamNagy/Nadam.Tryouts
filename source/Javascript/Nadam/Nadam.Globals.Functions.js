;
"use strict";

/// <summary>
/// 
/// </summary>
/// <returns></returns>
var GetById = function(id) {
    return document.getElementById(id);
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