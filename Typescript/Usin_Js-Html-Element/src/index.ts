console.log(echo("halika"));

var accConfig: AccordionElementConfig[] = [];// as AccordionElementConfig;
accConfig.push({
	title: "Hello god",
	contentElement: document.createElement("p")
} as AccordionElementConfig);

var acc: AccordionElement = new AccordionElement(accConfig);
document.body.append(acc);
