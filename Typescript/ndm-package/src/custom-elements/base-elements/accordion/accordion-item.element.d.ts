import { PrototypeHTMLElement } from "../../../htmlelement/prototype-html-element";
export declare class AccordionItemElement extends PrototypeHTMLElement {
    private templateId;
    private header;
    private body;
    constructor(parentId: string, index: string, child: HTMLElement, title?: string);
    Open(): void;
}
