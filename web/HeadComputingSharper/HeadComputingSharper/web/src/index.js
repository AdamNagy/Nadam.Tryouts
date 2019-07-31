import { Provider } from 'mithril-redux';
import configStore from './store';
import { HomeComponent } from './home/home.component';
import "./bootstrap/bootstrap.index";

const store = configStore({age: 30});
window.store = store;
m.mount(document.body, Provider.init(store, m, HomeComponent));