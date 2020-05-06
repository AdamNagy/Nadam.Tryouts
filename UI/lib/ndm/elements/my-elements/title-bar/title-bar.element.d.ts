import { PrototypeElement } from "../../../prototype-lib/prototype-element";
import { TitleBarConfig } from "./title-bar.config";
import "./title-bar.element.scss";
export declare class TitleBarElement extends PrototypeElement {
    constructor(config: TitleBarConfig);
    ChangeTitle(newTitle: string): void;
}
