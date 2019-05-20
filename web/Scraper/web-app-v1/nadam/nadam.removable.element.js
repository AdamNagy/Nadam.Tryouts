;
"use strict";

var RemovableElement = function (element) {

    var self = this;
    this.item = document.createElement("div");

    (function (_element) {

        self.item.append(_element);
        self.item.style.position = "relative";
        self.item.style.display = _element.style.display;

        var remover = document.createElement("div");
        remover.style.position = "absolute";
        remover.style.right = "10px";
        remover.style.top = "10px";
        remover.style.fontSize = "20px";
        remover.style.cursor = "pointer";
        remover.innerText = "X";

        remover.addEventListener("click", () => {

            self.item.remove();
        });
        self.item.append(remover);
    })(element);
};