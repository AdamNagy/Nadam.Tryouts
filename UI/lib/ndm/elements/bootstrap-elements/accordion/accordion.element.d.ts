import { PrototypeElement } from "../../../prototype-lib/prototype-element";
import { AccordionConfig } from "./accordion.config";
export declare class AccordionElement extends PrototypeElement {
    private numOfItems;
    readonly AccordionId: string;
    private accordions;
    constructor(config: AccordionConfig);
    WithItem(item: HTMLElement, title?: string): this;
    OpenAccordion(idx: number): this;
    OpenLastAccordion(): this;
}
