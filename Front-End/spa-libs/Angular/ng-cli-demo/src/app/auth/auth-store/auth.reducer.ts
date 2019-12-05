import { createReducer, on } from '@ngrx/store';
import { login, loginSuccess } from './auth.action';
import { AuthStoreModel, AccountModel, LoginRequestModel } from "../auth.model";

export const initialState: AuthStoreModel = {
	Request: null,
	Account: null,
	IsAuthenticated: false
};
 
const _authReducer = createReducer(
	initialState,
	on(
		login,
		(state,  payload: LoginRequestModel) => { return {...state, Request: payload}; }
	),
	on(
		loginSuccess,
		(state, payload: AccountModel) => { return {...state, Account: payload, IsAuthenticated: true}; }
	)
);
 
export function authReducer(state, action) {
  return _authReducer(state, action);
}
