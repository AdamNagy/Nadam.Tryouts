export interface IPage {
    Show(): void;
    Hide(): void;
    Render(): boolean;
    Body(): HTMLElement;
}
