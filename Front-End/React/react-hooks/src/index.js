import React, {createContext} from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';

export const TreesContext = createContext();
const trees = [
  {id: 1, type: "Maple"},
  {id: 2, type: "Oak"},
  {id: 3, type: "Family"},
  {id: 4, type: "Component"}
]

ReactDOM.render(
  <TreesContext.Provider value={trees}> 
    <App />
  </TreesContext.Provider>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
