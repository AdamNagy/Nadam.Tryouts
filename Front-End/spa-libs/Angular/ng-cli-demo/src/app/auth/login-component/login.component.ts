import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { AuthStoreModel, AccountModel } from '../auth.model';
import { loginRequest } from '../auth-store/auth.action';

@Component({
  selector: 'notes-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoginComponent implements OnInit {

	private isAuthenticated: boolean;
	private account: AccountModel;

	constructor(
		private store: Store<{auth: AuthStoreModel}>,
		private ref: ChangeDetectorRef
	) { }

	ngOnInit() {

		this.isAuthenticated = false;

		this.store.pipe(select('auth'))
			.subscribe((val) => {
				this.isAuthenticated = val.IsAuthenticated;
				this.account = val.Account;
				this.ref.markForCheck()
			});
	}

  	private onLoginClick(email: string, password: string) {

		const loginRequestModel = {
			Email: email,
			Password: password
		};

		this.store.dispatch(loginRequest(loginRequestModel));
	}
}
