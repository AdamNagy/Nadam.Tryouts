
interface PrototypeElement extends HTMLElement {
	WithId(): PrototypeElement;
}
// @ts-ignore
HTMLElement.prototype.WithId = function() {

}

// ts
function ndm_TS(): PrototypeElement {
	// @ts-ignore
	return document.createElement("div") as PrototypeElement;
}

// js
function ndm_JS() {
	// @ts-ignore
	return document.createElement("div");
}