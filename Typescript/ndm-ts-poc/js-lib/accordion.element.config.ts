// tslint:disable-next-line
declare interface AccordionElementModel {
	title: string;
	contentElement: HTMLElement;
}

// tslint:disable-next-line
declare interface AccordionElementConfig extends HtmlElementBaseConfig {
	items: AccordionElementModel[];
}
