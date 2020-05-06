import { PrototypeElement } from "../../../prototype-lib";
import { IColumnLaneConfig } from "./column-lane.config";
import "./column-lane.element.scss";
export declare class ColumnLaneElement extends PrototypeElement {
    private items;
    private currentConfig;
    private observer;
    private portraitImageWidth;
    constructor(config: IColumnLaneConfig, items: PrototypeElement[]);
    private Init;
    private LoadImage;
    private ClassifyImage;
}
