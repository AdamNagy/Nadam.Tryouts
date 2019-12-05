import { createSelector } from '@ngrx/store';
import { AuthStoreModel } from "../auth.model";
import { selectAuthState } from "../../reducers/core.state";

export const selectAuth = createSelector(
	selectAuthState,
	(state: AuthStoreModel) => state
  );
  
  export const selectIsAuthenticated = createSelector(
	selectAuthState,
	(state: AuthStoreModel) => state.IsAuthenticated
  );

  export const selectAccount = createSelector(
	selectAuthState,
	(state: AuthStoreModel) => state.Account
  );

  export const selectLoginRequest = createSelector(
	selectAuthState,
	(state: AuthStoreModel) => state.Request
  );
