import 'bootstrap/js/dist/modal.js';
import * as $ from "jquery";

import { SidePagerElement, Modal, ModalOpenerElement } from "./nadam/nadam.index";

// Side pager
let sidePager = new SidePagerElement();
sidePager.AddPage(document.createElement("p"));

document.getElementById("side-pager").addEventListener("click", () => {
	sidePager.AddPage(document.createElement("p"));
})

document.body.append(sidePager.view);

// Modal
document.body.append(Modal);
document.getElementById("modal-opener").addEventListener("click", () => {
	Modal.SetContent("Original image", document.createElement("p"));
	($("#nadam-modal") as any).modal("show");
})

