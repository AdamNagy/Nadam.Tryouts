import { createAction, props } from '@ngrx/store';
import { AccountModel, LoginRequestModel } from '../auth.model';

const PREFIX = '[AUTH]';

export const AUTH_ACTIONS = {
	loginRequest: `${PREFIX} login request`,
	loginSuccess: `${PREFIX} login success`,
	loginFail: `${PREFIX} login fail`
}

export const loginRequest = createAction(AUTH_ACTIONS.loginRequest, props<LoginRequestModel>());
export const loginSuccess = createAction(AUTH_ACTIONS.loginSuccess, props<AccountModel>());
export const loginFail = createAction(AUTH_ACTIONS.loginFail);
