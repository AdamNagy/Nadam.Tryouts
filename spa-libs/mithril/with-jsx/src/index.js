import m from "mithril";
import { MyComponent } from "./my-component/my-component.js";
import { MyView } from "./my-component/bigComponent";

m.render(document.body, "hello world");
var myComp = new MyComponent(document.body);
myComp.Render();
m.mount(document.body, MyView);