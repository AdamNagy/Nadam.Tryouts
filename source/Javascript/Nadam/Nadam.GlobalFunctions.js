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

var IsPrimitive = function(variable) {
    var type = typeof variable;

    if (type === "number" || type === "string" || type === "boolean") {
        return true;
    }

    return false;
}

var IsFunction = function(variable) {

}

var IsObject = function(variable) {

}