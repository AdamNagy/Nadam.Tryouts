declare interface HTMLElement {

	// attribute
	WithAttribute(key: string, value: string): HTMLElement;

	WithoutAttribute(key: string, value: string): HTMLElement;

	// style
	WithStyle(key: string, value: string): HTMLElement;

	WithCss(cssObj: any): HTMLElement;

	// class
	WithClass(name: string): HTMLElement;

	WithClasses(classNames: string[]): HTMLElement;

	WithoutClass(className: string): HTMLElement;

	// inner text/value
	WithInnerText(text: string): HTMLElement;

	WithValue(val: string): HTMLElement;

	// id
	WithId(id: string): HTMLElement;

	// child element
	WithChild(child: HTMLElement): HTMLElement;

	WithChildren(children: HTMLElement[]): HTMLElement;

	WithoutChildren(): HTMLElement;

	// event listener
	WithEventListener(event: string, func: any): HTMLElement;

	WithoutEventListener(eventName: string, event: any): HTMLElement;

	WithOnClick(func: any): HTMLElement;
	
	WithOnLoad(func: any): HTMLElement;

	// other
	ToParent(parent: HTMLElement): HTMLElement;
}
