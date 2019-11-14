import { PrototypeHTMLElement } from "../../../htmlelement/prototype-html-element";
declare class ModalElement extends PrototypeHTMLElement {
    private TemplateId;
    private Body;
    private Title;
    constructor();
    WithContent(title: string, bodyContent: HTMLElement): this;
}
export declare const Modal: ModalElement;
export {};
