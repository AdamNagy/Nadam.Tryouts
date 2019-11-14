export declare class PrototypeHTMLElement extends HTMLElement {
    WithAttribute(key: string, value: string): PrototypeHTMLElement;
    WithStyle(key: any, value: string): PrototypeHTMLElement;
    WithClass(name: string): PrototypeHTMLElement;
    WithInnerText(text: string): this;
    WithId(id: string): this;
}
