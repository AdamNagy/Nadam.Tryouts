import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginRequestModel, AccountModel } from '../auth.model';
import { Observable, of } from 'rxjs';
import * as _ from "lodash";

@Injectable({
  providedIn: 'root'
})
export class LoginService {

	private authAPi = "https://localhost:44312/api/Account/login";

	constructor(
		private http: HttpClient
	) { 
	}

	public Login(requestModel: LoginRequestModel): Observable<AccountModel> {

		return this.http.post<AccountModel>(this.authAPi, requestModel);
	}

}
