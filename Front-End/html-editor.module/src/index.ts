import "../js_modules/tinymce.min.js";
// @ts-ignore
tinymce.init({selector: "#tinymce-txtarea"});

const focusLayer = document.getElementById("focus-layer");

const htmlCodeContainer = document.getElementById("html-code-container") as HTMLInputElement;

const getBtn = document.getElementById("get-html");
const setBtn = document.getElementById("set-html");
let tinyMceContainer: HTMLElement;

setTimeout(() => {
	getBtn.removeAttribute("disabled");
	setBtn.removeAttribute("disabled");
	focusLayer.style.visibility = "hidden";
	tinyMceContainer = (document.getElementById("tinymce-txtarea_ifr") as HTMLIFrameElement).contentWindow.document.body;

	document.querySelector("body > div.tox-tinymce-aux").remove();
}, 1000);

getBtn.addEventListener("click", () => getHtml());
const getHtml = () => {
	htmlCodeContainer.value = tinyMceContainer.innerHTML;
};

setBtn.addEventListener("click", () => setHtml());
const setHtml = () => {
	tinyMceContainer.innerHTML = htmlCodeContainer.value;
};
