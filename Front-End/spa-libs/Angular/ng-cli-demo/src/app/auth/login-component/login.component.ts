import { Component, OnInit } from '@angular/core';
import { selectIsAuthenticated, selectAccount } from "../auth-store/auth.selector";
import { LoginService } from "../auth-service/auth.service";
@Component({
  selector: 'notes-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

	private isAuthenticated: boolean;

	constructor(
		private service: LoginService
	) { }

  ngOnInit() {

  }

  private onLoginClick() {

}

}
