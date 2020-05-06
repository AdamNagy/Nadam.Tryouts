import { PrototypeElement } from "../../../prototype-lib/index";
import "./removable.element.scss";
export declare class RemovableElement extends PrototypeElement {
    private child;
    private TemplateId;
    readonly Child: HTMLElement;
    constructor(item: HTMLElement);
    private onRemove;
    OnRemove(func: any): this;
    private FireOnRemove;
}
