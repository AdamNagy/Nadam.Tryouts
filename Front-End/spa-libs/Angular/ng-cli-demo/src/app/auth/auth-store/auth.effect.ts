import { Injectable } from '@angular/core';
import { Actions, ofType, Effect } from '@ngrx/effects';
import { of } from 'rxjs';
import { map, catchError, switchMap } from 'rxjs/operators';
import { LoginService } from '../auth-service/auth.service';
import { AUTH_ACTIONS, loginSuccess } from "./auth.action";
import { AccountModel } from '../auth.model';
import { NoteService } from 'src/app/note/service/note.service';

@Injectable()
export class AuthEffects {
 
	@Effect()
	login = this.actions.pipe(
		ofType(AUTH_ACTIONS.loginRequest),
		switchMap((payload) =>
			this.authService.Login(payload).pipe(
				map(account => loginSuccess(account)),
				catchError(error => {
					console.log(error);
					return of("");}),
			),
		),
	);

	// @Effect()
	// getNotes = this.noteAction.pipe(
	// 	ofType(AUTH_ACTIONS.loginSuccess),
	// 	switchMap((account: AccountModel, index) => 
	// 		this.authService.Login(account).pipe(
	// 			map(account => this.noteService.Get(account.id)),
	// 			catchError(error => of("new `GetCustomersFailed`(error)")),
	// 		)
	// ));

	constructor(
		private actions: Actions<AccountModel | any>,
		private noteAction: Actions<string | any>,
		private authService: LoginService,
		private noteService: NoteService
	) {}
}