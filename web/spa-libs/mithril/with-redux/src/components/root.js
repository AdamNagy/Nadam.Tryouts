import m from 'mithril';
import {BaseComp, ChildComp} from './components';

class Root {
  view() {
    return m('div', [BaseComp]);
  }
}

export default new Root();
