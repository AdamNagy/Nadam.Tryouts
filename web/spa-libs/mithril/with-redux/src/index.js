import m from 'mithril';
import {Provider} from 'mithril-redux';
import configStore from './store/store';
import Root from './components/root';

const store = configStore({name: 'World!', age: 30});

m.mount(document.body, Provider.init(store, m, Root));