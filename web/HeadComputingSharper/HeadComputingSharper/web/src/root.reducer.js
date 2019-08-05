import { combineReducers } from 'redux';
import { gameReducer } from "./home/home.reducer";

const rootReducer = combineReducers({
	gameReducer
  });

export default rootReducer;