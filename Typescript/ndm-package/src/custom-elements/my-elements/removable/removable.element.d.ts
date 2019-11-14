import { PrototypeHTMLElement } from "../../../htmlelement/prototype-html-element";
import "./removable.element.scss";
export declare class RemovableElement extends PrototypeHTMLElement {
    private child;
    private TemplateId;
    readonly Child: HTMLElement;
    constructor(item: HTMLElement);
    private onRemove;
    OnRemove(func: any): void;
    private FireOnRemove;
}
