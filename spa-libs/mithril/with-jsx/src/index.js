import m from "mithril";
import { MyJSXComponent } from "./my-component/my-jsx.component";
import { StandardComponent } from "./my-component/standard.component";

m.render(document.body, "hello world");
var myComp = new StandardComponent(document.body);
myComp.Render();
m.mount(document.body, MyJSXComponent);