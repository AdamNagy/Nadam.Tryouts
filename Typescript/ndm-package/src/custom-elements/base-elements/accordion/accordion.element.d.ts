import { PrototypeHTMLElement } from "../../../htmlelement/prototype-html-element";
export declare class AccordionElement extends PrototypeHTMLElement {
    private numOfItems;
    readonly AccordionId: string;
    private accordions;
    constructor(id: string);
    WithItem(item: HTMLElement, title?: string): this;
    OpenAccordion(idx: number): this;
}
