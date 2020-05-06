import { PrototypeElement } from "../../../prototype-lib/index";
declare class ModalElement extends PrototypeElement {
    private Title;
    constructor();
    WithContent(title: string, bodyContent: HTMLElement): this;
}
export declare const Modal: ModalElement;
export {};
