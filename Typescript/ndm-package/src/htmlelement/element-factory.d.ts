import { PrototypeHTMLElement } from "./prototype-html-element";
export declare class ElementFactory {
    static Create(elementName: string): PrototypeHTMLElement;
    static ConstructFromTemplate(tamplateId: string): any;
}
