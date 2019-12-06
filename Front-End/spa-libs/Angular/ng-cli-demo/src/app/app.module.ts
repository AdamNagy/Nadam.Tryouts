import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './auth/login-component/login.component';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { authReducer } from './auth/auth-store/auth.reducer';
import { NoteComponent } from './note/component/note.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';

import {
	MatButtonModule, MatCardModule, MatDialogModule, MatInputModule, MatTableModule,
	MatToolbarModule, MatMenuModule,MatIconModule, MatProgressSpinnerModule
  } from '@angular/material';
import { AuthEffects } from './auth/auth-store/auth.effect';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    NoteComponent
  ],
  imports: [
    BrowserModule,
	HttpClientModule,
	AppRoutingModule,
	EffectsModule.forRoot([AuthEffects]),
	StoreModule.forRoot({ auth: authReducer }),
	
	BrowserAnimationsModule,
	MatToolbarModule,
	MatButtonModule, 
	MatCardModule,
	MatInputModule,
	MatDialogModule,
	MatTableModule,
	MatMenuModule,
	MatIconModule,
	MatProgressSpinnerModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
