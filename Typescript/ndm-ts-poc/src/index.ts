import { fromEvent, merge, of } from "rxjs";
import { BehaviorSubject } from "rxjs/BehaviorSubject";

const accConfig: AccordionElementConfig[] = []; // as AccordionElementConfig;
accConfig.push(
	{
		title: "Hello god",
		contentElement: document.createElement("p").WithInnerText("csumika"),
	},
	{
		title: "Hello god 2",
		contentElement: document.createElement("p").WithInnerText("megint"),
	});

const acc: AccordionElement = new AccordionElement(accConfig);
document.body.append(acc);
