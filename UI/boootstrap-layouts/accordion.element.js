// this component does not support pre-render or in template usage.
// has to be used from script, and only from script

class Accordion extends HTMLElement {
	
	TemplateId = "accordion";
	Container;
	NumOfItems;
	Id:
	
	constructor(id) {
		super();		
	
		this.Id = id;
		
		const template = document.getElementById(this.TemplateId);
		const node = document.importNode(template.content, true);
		
		this.Container = node.querySelector("div.accordion");
		this.append(node);
	}
	
	AddItem(item) {
		
		let accItem = new AccordionItem(++(this.NumOfItems), item);
		this.Container.appendChild(accItem);
	}
}
	
class AccordionItem extends HTMLElement {
	
	TemplateId = "accordion-item";
	Index;	
	Content;
	
	constructor(index, content) {
		super();
				
		this.Index = index;
		this.Content = content;
		
		const template = document.getElementById(this.TemplateId);
		const node = document.importNode(template.content, true);
		
		this.Container = node.querySelector("div.accordion");
		this.Container.appendChild(this.Content);
		this.append(node);
	}
}

customElements.define('nadam-accordion', Accordion);
customElements.define('nadam-accordion-item', AccordionItem);