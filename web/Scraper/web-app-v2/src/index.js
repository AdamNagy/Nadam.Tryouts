import "babel-polyfill";
import { App } from "./app/webhack.app";

jQueryBridget( 'masonry', Masonry, $ );

console.log("app starting");
var app = new App();

var requestBtn = document.getElementById("request-btn");
requestBtn.addEventListener("click", () => {
    app.Request();
});

var requestP10Btn = document.getElementById("request-p10-btn");
requestP10Btn.addEventListener("click", () => {
    app.RequestP10();
});


var clearBtn = document.getElementById("clear-btn");
clearBtn.addEventListener("click", () => {
	app.Clear();
	document.getElementById("page-input").value = 1;
});

// request-p10-btn

document.getElementById("page-input").value = 1;

