console.log(echo("halika"));

var accConfig = [];// as AccordionElementConfig;
accConfig.push({
	title: "Hello god",
	contentElement: document.createElement("p")
} as AccordionElementConfig)

var acc = new AccordionElement(accConfig);
document.body.append(acc);
