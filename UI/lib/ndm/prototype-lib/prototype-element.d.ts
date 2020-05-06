export interface IPrototypeElement {
    $nid(nid: string): PrototypeElement;
    WithAttribute(key: string, value: string): PrototypeElement;
    WithoutAttribute(key: string): PrototypeElement;
    /************************************************************************/
    WithStyle(key: any, value: string): PrototypeElement;
    /************************************************************************/
    WithClass(name: string): PrototypeElement;
    /************************************************************************/
    WithInnerText(text: string): PrototypeElement;
    /************************************************************************/
    WithId(id: string): PrototypeElement;
    /************************************************************************/
    WithChild(child: HTMLElement): PrototypeElement;
    WithChildren(children: HTMLElement[] | HTMLCollection): PrototypeElement;
    WithoutChildren(): PrototypeElement;
    /************************************************************************/
    WithEventListener(event: string, func: any): PrototypeElement;
    WithoutEventListener(event: string): PrototypeElement;
    WithOnClick(func: any): PrototypeElement;
}
export declare class PrototypeElement extends HTMLElement {
    constructor(templateId?: string, templateString?: string);
    $nid(nid?: string): PrototypeElement;
    $class(nid?: string): PrototypeElement;
    $(selector?: string): PrototypeElement;
    Value(): string;
    Height(): string;
    Width(): string;
    WithAttribute(key: string, value: string): PrototypeElement;
    WithoutAttribute(key: string): PrototypeElement;
    WithStyle(key: any, value: string): PrototypeElement;
    WithClass(name: string): PrototypeElement;
    WithInnerText(text: string): PrototypeElement;
    WithId(id: string): PrototypeElement;
    WithChild(child: HTMLElement): PrototypeElement;
    WithChildren(children: HTMLElement[] | HTMLCollection): PrototypeElement;
    WithoutChildren(): PrototypeElement;
    WithEventListener(event: string, func: any): PrototypeElement;
    WithoutEventListener(event: string): PrototypeElement;
    WithOnClick(func: any): PrototypeElement;
    WithOnLoad(func: any): PrototypeElement;
}
