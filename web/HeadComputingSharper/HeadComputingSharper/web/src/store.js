import { createStore, applyMiddleware } from 'redux';
import thunkMiddleware from 'redux-thunk';
import { redrawMiddleware } from 'mithril-redux';
import rootReducer from './root.reducer';

export default function configureStore(initialState) {
  const createModifiedStore = applyMiddleware(
    thunkMiddleware,
    redrawMiddleware
  )(createStore);
  return createModifiedStore(rootReducer, initialState);
}