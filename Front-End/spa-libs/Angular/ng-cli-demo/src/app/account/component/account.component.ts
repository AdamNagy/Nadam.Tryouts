import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { UserModel } from '../account.model';

@Component({
  selector: 'grn-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoginComponent implements OnInit {

	private isAuthenticated: boolean;
	private account: UserModel;

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

		
	}
}
