import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginRequestModel, UserModel } from '../account.model';
import { Observable, of } from 'rxjs';
import * as _ from "lodash";

@Injectable({
  providedIn: 'root'
})
export class AccountService {

	get isAuthenticated(): boolean {
        return this._isAuthenticated;
    }
	// move this to config file
	private authAPi = "https://localhost:44312/api/account/login";
	private cache: Observable<UserModel>;
	private _isAuthenticated: boolean;

	constructor(
		private http: HttpClient
	) { 
	}

	public login(requestModel: LoginRequestModel): Observable<UserModel> {
		if(!this._isAuthenticated)
			this.cache = this.sendLoginRequest(requestModel);
		
		this.cache.subscribe(response => this._isAuthenticated = true);
		return this.cache;
	}

	public logOut(): void {
		this._isAuthenticated = false;
		this.cache = null;
	}

	private sendLoginRequest(requestModel: LoginRequestModel): Observable<UserModel> {

		return this.http.post<UserModel>(this.authAPi, requestModel);
	}

}
