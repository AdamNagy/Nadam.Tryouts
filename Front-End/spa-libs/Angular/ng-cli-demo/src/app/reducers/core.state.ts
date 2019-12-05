import {
	ActionReducerMap,
//	MetaReducer,
	createFeatureSelector
  } from '@ngrx/store';
  import { routerReducer, RouterReducerState } from '@ngrx/router-store';
 // import { storeFreeze } from 'ngrx-store-freeze';
  
 // import { environment } from '../../environments/environment';
  
//   import { initStateFromLocalStorage } from './meta-reducers/init-state-from-local-storage.reducer';
//   import { debug } from './meta-reducers/debug.reducer';
  import { AuthStoreModel } from '../auth/auth.model';
  import { authReducer } from '../auth/auth-store/auth.reducer';
  import { RouterStateUrl } from './router.state';
  
  export const reducers: ActionReducerMap<AppState> = {
	auth: authReducer,
	router: routerReducer
  };
  
//   export const metaReducers: MetaReducer<AppState>[] = [
// 	initStateFromLocalStorage
//   ];

//   if (!environment.production) {
// 	metaReducers.unshift(storeFreeze);
// 	if (!environment.test) {
// 	  metaReducers.unshift(debug);
// 	}
//   }
  
  export const selectAuthState = createFeatureSelector<AppState, AuthStoreModel>(
	'auth'
  );
  
  export const selectRouterState = createFeatureSelector<
	AppState,
	RouterReducerState<RouterStateUrl>
  >('router');
  
  export interface AppState {
	auth: AuthStoreModel;
	router: RouterReducerState<RouterStateUrl>;
  }
  