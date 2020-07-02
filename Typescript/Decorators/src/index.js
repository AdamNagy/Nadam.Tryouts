"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
function bindClick(target, key, descriptor) {
    console.log({ target });
    console.log({ key });
    console.log({ descriptor });
    return descriptor;
}
function bindValue(target, key) {
    console.log({ target });
    console.log({ key });
}
class TestElement extends HTMLElement {
    constructor() {
        super();
        this.xprop = "hello";
        this.button = document.createElement("button");
        this.button.innerText = "click it";
        this.append(this.button);
        this.nameInput = document.createElement("input");
        this.nameInput.setAttribute("type", "text");
        this.nameInput.setAttribute("id", "nameInput");
        this.nameInput.value = "add something";
        this.append(this.nameInput);
    }
    myButton(targetElement = "") {
        console.log("clicked");
    }
}
__decorate([
    bindValue
], TestElement.prototype, "nameInput", void 0);
__decorate([
    bindClick
], TestElement.prototype, "myButton", null);
customElements.define("test-element", TestElement);
document.body.append(new TestElement());
