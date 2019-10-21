export class Accordion extends HTMLElement {
	
	TemplateId = "accordion";
	Container: HTMLElement;
	NumOfItems: number;
	
	constructor() {
		super();
	}
	
	AddItem(item: HTMLElement) {
		
		let accItem = new AccordionItem(++NumOfItems);
		accItem.SetBody(item);
		this.Container.appendChild(accItem);
	}
}

	
export class AccordionItem extends HTMLElement {
	
	TemplateId = "accordion-item";
	Index: number;
	
	
	constructor(idx: number) {
		super();
	}
	
	SetBody(body: HTMLElement) {
		
	}
}