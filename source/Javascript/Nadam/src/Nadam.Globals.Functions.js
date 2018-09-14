;
"use strict";

/// <DOM_related_function>
var GetById = function(id) {

    var sameIds = document.getElementById(id);

    if (sameIds !== null && sameIds.length !== undefined && sameIds.length >= 1) {
        return sameIds[0].AddProperty("Type", sameIds[0].nodeName.toLowerCase());
    }
    [1, 2, 3, 4, 5].filter((item) => item / 2 === 0);
    return sameIds.AddProperty("Type", sameIds.nodeName.toLowerCase());
}

var GetByClass = function(className) {

    var sameClasses = document.getElementsByClassName(className);
    // sameClasses.Each((item) => item.AddProperty("Type", item.nodeName.toLowerCase()));

    return sameClasses;
}

var GetByTagName = function(tagName) {

    return document.getElementsByTagName(tagName);
}

var RemoveAllFromDom = function(tagName) {

    var element = GetByTagName(tagName)[0];
    while (element !== null) {
        element.remove();
        element = GetByTagName(tagName)[0];
    }
}

/// </DOM_related_function>

/// <Type_related_function>
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

/// </Type_related_function>