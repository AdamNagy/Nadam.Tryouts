import React from 'react';
import ReactDOM from 'react-dom';
import App from './App.js';
import { configureStore } from "./store";
import { Provider } from "react-redux"
import { persistStore } from "redux-persist";
import { PersistGate } from "redux-persist/lib/integration/react";

const store = configureStore();
const persistor = persistStore(store);

ReactDOM.render(
    <Provider store={store}>
        <PersistGate persistor={persistor} loading={<div>loading...</div>}>
            <App /> 
        </PersistGate>
    </Provider>,
    document.getElementById('root')
);