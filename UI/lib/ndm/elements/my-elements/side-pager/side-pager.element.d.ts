import { PrototypeElement } from "../../../prototype-lib/prototype-element";
import "./side-pager.element.scss";
export declare class SidePagerElement extends PrototypeElement {
    private Pages;
    private OpenPages;
    private PageIdx;
    private PageWidth;
    private SpaceBetweenPages;
    private onAllClosed;
    private onAnyOpen;
    constructor();
    TogglePage(pageId: number): void;
    OpenPage(pageId: number): void;
    ClosePage(pageId: number): void;
    RemovePage(pageId: number): void;
    CloseAll(): void;
    AddPage(rootElement: PrototypeElement): void;
    OnAllClosed(func: any): void;
    OnAnyOpen(func: any): void;
    private AddEmptyPage;
    private GetIndex;
    private FireOnAnyOpen;
    private FireOnAllClosed;
}
