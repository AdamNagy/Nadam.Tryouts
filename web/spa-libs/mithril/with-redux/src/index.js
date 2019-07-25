import m from "mithril";

window.store = configStore([]);

import { HeadCounterComponent } from './components/head-counter.component';
m.mount(document.body, HeadCounterComponent);
