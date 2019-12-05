import { createAction, props } from '@ngrx/store';
import { AccountModel, LoginRequestModel } from '../auth.model';

const PREFIX = '[AUTH]';

export const login = createAction(`${PREFIX} login request`, props<LoginRequestModel>());
export const loginSuccess = createAction(`${PREFIX} login success`, props<AccountModel>());
