import m from 'mithril';
import {NameBox, AgeBox, NameBoxJsx} from './components';

class Root {
  view() {
    return m('div', [NameBox, AgeBox, NameBoxJsx]);
  }
}

export default new Root();
