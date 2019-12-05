import { createSelector } from '@ngrx/store';
import { AuthStoreModel } from '../auth.model';
import { AppState } from 'src/app/reducers/core.state';
 
export const selectFeature = (state: AppState) => state.auth;
 
export const selectIsAuthenticated = createSelector(
  selectFeature,
  (state: AuthStoreModel) => state.IsAuthenticated
);