export interface LoginRequestModel {
    Email: string;
    Password: string;
}

export interface UserModel {
	Password: string;
	token: string;
	firstName: string;
	lastName: string;
	Email: string;
	id: string;
}
