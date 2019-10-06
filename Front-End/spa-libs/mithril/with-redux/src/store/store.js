import { createStore, applyMiddleware } from 'redux';
import thunkMiddleware from 'redux-thunk';
import rootReducer from './reducers';

function configureStore(initialState) {
  const createModifiedStore = applyMiddleware(
    thunkMiddleware
  )(createStore);
  return createModifiedStore(rootReducer, initialState);
}

export const store = configureStore({name: 'Adam N!', age: 30});