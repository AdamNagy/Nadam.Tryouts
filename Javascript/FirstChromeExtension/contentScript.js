window.Nadam = window.Nadam || {};
Nadam.Webhack = Nadam.Webhack || {};

Nadam.Webhack.Index = new function() {
	var Name = "Nadam.Webhack.Index";

	this.PrintMyName = () => {console.log(Name)}

	this.Init = function() {
		var controlPanel = document.createElement("DIV");
		controlPanel.setAttribute("id", "theButton");
		
		var btn_getImageSrcs = document.createElement("BUTTON");
		btn_getImageSrcs.setAttribute("id", "btn-get-image-srcs");
		btn_getImageSrcs.innerHTML = "Get image sources";
		btn_getImageSrcs.style.margin = "5px";
		controlPanel.appendChild(btn_getImageSrcs);
		
		controlPanel.style.width = "auto";
		controlPanel.style.height = "auto";
		controlPanel.style.position = "fixed";
		controlPanel.style.top = "50%";
		controlPanel.style.opacity = "0.8";
		controlPanel.style.zIndex = "500";
		controlPanel.style.backgroundColor = "rgba(125, 125, 125, 0.5)";
		
		document.body.appendChild(controlPanel);
	}
};

// var port = chrome.runtime.connect();

// window.addEventListener("message", function(event) {
//   // We only accept messages from ourselves
// //   if (event.source != window)
// //     return;

//   if (event.data.type && (event.data.type == "FROM_PAGE")) {
//     console.log("Content script received: " + event.data.text);
// 	port.postMessage(event.data.text);
// 	Nadam.Webhack.Index.Init();
//   }
// }, false);

console.log("content script loaded");
Nadam.Webhack.Index.Init();