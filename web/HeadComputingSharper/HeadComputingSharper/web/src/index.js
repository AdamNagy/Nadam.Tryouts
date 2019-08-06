import { Provider } from 'mithril-redux';
import configStore from './store';
import { HomeComponent } from './home/home.component';
// import "./bootstrap/bootstrap.index";
import { GameType } from "./home/home.models";

import './bootstrap/bootstrap-4.3.1-dist/css/bootstrap.min.css';
import './bootstrap/bootstrap-4.3.1-dist/css/bootstrap-grid.min.css';
import './bootstrap/bootstrap-4.3.1-dist/css/bootstrap-reboot.min.css';

const store = configStore({

	level: 1,
	type: GameType.addition,
	left: "3",
	right: "5",
	solution: "8"
});
window.store = store;
m.mount(document.body, Provider.init(store, m, HomeComponent));