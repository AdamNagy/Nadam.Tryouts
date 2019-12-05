import { Injectable } from '@angular/core';
import { Actions, ofType, Effect } from '@ngrx/effects';
import { of } from 'rxjs';
import { map, catchError, switchMap } from 'rxjs/operators';
import { LoginService } from '../auth-service/auth.service';
import { AUTH_ACTIONS, loginSuccess } from "./auth.action";
import { AccountModel } from '../auth.model';

@Injectable()
export class AuthEffects {
 
	@Effect()
	login = this.actions.pipe(
		ofType(AUTH_ACTIONS.loginRequest),
		switchMap((payload) =>
			this.authService.Login(payload).pipe(
				map(account => loginSuccess(account)),
				catchError(error => of("new `GetCustomersFailed`(error)")),
			),
		),
	);

	constructor(
		private actions: Actions<AccountModel | any>,
		private authService: LoginService
	) {}
}