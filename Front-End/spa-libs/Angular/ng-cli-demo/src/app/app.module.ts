import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/component/login.component';
import { StoreModule } from '@ngrx/store';
import { loginReducer } from './reducers';
import { NoteComponent } from './note/component/note.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import {
	MatButtonModule, MatCardModule, MatDialogModule, MatInputModule, MatTableModule,
	MatToolbarModule, MatMenuModule,MatIconModule, MatProgressSpinnerModule
  } from '@angular/material';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    NoteComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    StoreModule.forRoot(loginReducer, {
      runtimeChecks: {
        strictStateImmutability: true,
        strictActionImmutability: true
      }
    }),
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
