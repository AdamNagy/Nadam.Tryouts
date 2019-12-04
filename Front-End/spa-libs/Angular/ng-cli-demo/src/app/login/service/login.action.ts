import { createAction, props } from '@ngrx/store';
import { LoginModel } from './account.model';

const PREFIX = '[LOGIN]';

export const login = createAction(PREFIX + ' request', props<LoginModel>());
export const loginSuccess = createAction(PREFIX + ' success', );
