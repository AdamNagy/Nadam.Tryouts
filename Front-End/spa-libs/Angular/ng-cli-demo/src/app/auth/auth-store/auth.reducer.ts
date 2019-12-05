import { createReducer, on } from '@ngrx/store';
import { loginRequest, loginSuccess } from './auth.action';
import { AuthStoreModel, AccountModel, LoginRequestModel } from "../auth.model";

export const initialState: AuthStoreModel = {
	Request: null,
	Account: null,
	IsAuthenticated: false
};
 
const _authReducer = createReducer(
	initialState,
	on(
		loginSuccess,
		(state, payload: AccountModel) => {
			const newState = {...state, Account: payload, IsAuthenticated: true};
			console.log(newState);
			return newState;
		}
	),
	on(
		loginRequest,
		(state,  payload: LoginRequestModel) =>  {
			const newState = {...state, Request: payload };
			console.log(newState);
			return newState;
		}
	),
);
 
export function authReducer(state, action) {
  return _authReducer(state, action);
}
