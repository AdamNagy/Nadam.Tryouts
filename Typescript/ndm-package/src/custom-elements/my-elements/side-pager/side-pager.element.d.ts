import { PrototypeHTMLElement } from "../../../htmlelement/prototype-html-element";
import "./side-pager.element.scss";
export declare class SidePagerElement extends PrototypeHTMLElement {
    private TemplateId;
    private Pages;
    private PageWidth;
    private SpaceBetweenPages;
    private OpenPages;
    private onAllClosed;
    private onAnyOpen;
    constructor();
    OpenPage(pageId: number): void;
    ClosePage(pageId: number): void;
    RemovePage(pageId: number): void;
    CloseAll(): void;
    AddPage(rootElement: HTMLElement): void;
    OnAllClosed(func: any): void;
    OnAnyOpen(func: any): void;
    private CreatePage;
    private GetIndex;
    private FireOnAnyOpen;
    private FireOnAllClosed;
}
