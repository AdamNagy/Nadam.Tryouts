import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { AuthStoreModel, AccountModel } from '../auth.model';

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
		private ref: ChangeDetectorRef
	) { }

	ngOnInit() {

		this.isAuthenticated = false;
	}

  	private onLoginClick(email: string, password: string) {

		const loginRequestModel = {
			Email: email,
			Password: password
		};

		// this.store.dispatch(loginRequest(loginRequestModel));
	}
}
