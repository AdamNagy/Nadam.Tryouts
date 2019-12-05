export interface LoginRequestModel {
    Email: string;
    Password: string;
}

export interface AccountModel {
	Password: string;
	Token: string;
	FirstName: string;
	LastName: string;
	Email: string;
}

export interface AuthStoreModel {

	Request: LoginRequestModel;
	Account: AccountModel;
	IsAuthenticated: boolean;
}