import { Injectable } from '@angular/core';
import { LoginRequestModel, AccountModel } from '../auth.model';
import { Observable, of } from 'rxjs';
import * as _ from "lodash";

@Injectable({
  providedIn: 'root'
})
export class LoginService {

	private accounts: AccountModel[];
	constructor(
	) { 
		this.accounts = [];
		this.accounts.push(
			{
				FirstName: "cement",
				LastName: "elek",
				Password: "mind123",
				Token: "asd123.asdqwe123.wer234.wer234wer",
				Email: "cement.elek@gmail.com"
			}
		)
	}

	public Login(requestModel: LoginRequestModel): Observable<AccountModel> {

		const found = _.filter(this.accounts, (item) => 
			item.Email === requestModel.Email && item.Password === requestModel.Password);

		return of(found);
	}

}
