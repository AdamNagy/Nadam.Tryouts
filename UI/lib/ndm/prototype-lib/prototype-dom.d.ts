import { PrototypeElement } from "./prototype-element";
export declare class PrototypeDom {
    static CreateFromRawHtml(htmlString: string, templateId: string): PrototypeElement;
    static CreateFromTemplate(templateId: string): PrototypeElement;
    static Create(elementName: string): PrototypeElement;
    static CreateContainer(): PrototypeElement;
    static CreateFluidContainer(): PrototypeElement;
    static CreateRow(): PrototypeElement;
}
