import { createStore, dispatch, getState } from 'redux';
import m from "mithril";
// import rootReducer from './store/reducers';
import tasksReducer from './store/reducers/head-counter.reducer';
import { addTask } from './store/actions/head-counter.actions';

window.store = createStore(tasksReducer);
window.store.dispatch(addTask({a: 3, b: 5, sum: 8}));
window.store.getState();


import { HeadCounterComponent } from './components/head-counter.component';
m.mount(document.body, HeadCounterComponent);
m.redraw();