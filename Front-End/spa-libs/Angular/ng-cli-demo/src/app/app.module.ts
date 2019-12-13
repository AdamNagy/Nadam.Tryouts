import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './auth/login-component/login.component';
import { NoteComponent } from './note/component/note.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';

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
	HttpClientModule,
	AppRoutingModule,
	
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
