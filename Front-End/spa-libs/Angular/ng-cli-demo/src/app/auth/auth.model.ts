export interface LoginRequestModel {
    Email: string;
    Password: string;
}

export interface AccountModel {
	Password: string;
	token: string;
	firstName: string;
	lastName: string;
	Email: string;
	id: string;
}

export interface AuthStoreModel {

	Request: LoginRequestModel;
	Account: AccountModel;
	IsAuthenticated: boolean;
}