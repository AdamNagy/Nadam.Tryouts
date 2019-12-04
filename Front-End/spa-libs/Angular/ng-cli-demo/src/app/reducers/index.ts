import { createReducer, on } from '@ngrx/store';
import { login, loginSuccess } from '../login/service/login.action';
import { Account, LoginModel } from "../login/service/account.model";
// import { stat } from 'fs';

interface IAccountState {
	Request: LoginModel,
	Account: Account
}

// const initialState: IAccountState {
// 	Request = undefined,
// 	Account = undefined
// }

export const initialState: IAccountState = {
	Account: null,
	Request: null
};
 
const _loginReducer = createReducer(
	initialState,
	on(
		login,
		(state: IAccountState,  model: LoginModel) => { return {...state, Request: model}; }
	),
);
 
export function loginReducer(state, action) {
  return _loginReducer(state, action);
}
