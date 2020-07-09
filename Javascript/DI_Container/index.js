"use strict";

// add one instance to window.app but
// or can be done in the app index.js or something
window.app = window.app || {};
window.app.registry = new Registry();

window.app.registry.registerElement("my-element", MyElement);

var element = window.app.registry.resolve("my-element", {});
element === undefined ? console.error("element is undefioned") : console.log("passed");

var element2 = window.app.registry.resolve("my-element-not-exist", {});
element2 === undefined ? console.log("passed: it is undefined") : console.error("something wrong");
