import m from 'mithril';
import {store} from './store/store';
import Root from './mithril-redux-components/root';
// import { TodosComponent } from "./mithril-components/simple-component";
import { TodosComponent } from "./mithril-tsx-component/other";

// m.mount(document.body, Provider.init(store, m, Root));
m.mount(document.body, new TodosComponent());