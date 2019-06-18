import "babel-polyfill";
import 'bootstrap/dist/css/bootstrap.min.css';

import { App } from "./app/webhack.app";
import { RegisterCustomElements } from "./nadam/nadam.custom-elements.index";


RegisterCustomElements();
console.log("app starting");
var app: App = new App();

var requestBtn: Element = document.getElementById("request-btn");
requestBtn.addEventListener("click", () => {
    app.Request();
});

var requestP10Btn: Element = document.getElementById("request-p10-btn");
requestP10Btn.addEventListener("click", () => {
    app.RequestP10();
});


var clearBtn: Element = document.getElementById("clear-btn");
clearBtn.addEventListener("click", () => {
	app.Clear();
	(document.getElementById("page-input") as HTMLInputElement).value = "1";
});

// request-p10-btn
(document.getElementById("page-input") as HTMLInputElement).value = "1";

