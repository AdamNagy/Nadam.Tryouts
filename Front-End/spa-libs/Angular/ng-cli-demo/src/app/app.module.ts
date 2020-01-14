import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './account/component/account.component';
import { NoteComponent } from './note/component/note.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';

import {
	MatButtonModule, MatCardModule, MatDialogModule, MatInputModule, MatTableModule,
	MatToolbarModule, MatMenuModule,MatIconModule, MatProgressSpinnerModule
  } from '@angular/material';
import { TestNgSchemanticsComponent } from './test-ng-schemantics/test-ng-schemantics.component';
import { MatSelectModule } from '@angular/material/select';
import { MatRadioModule } from '@angular/material/radio';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    NoteComponent,
    TestNgSchemanticsComponent
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
	MatProgressSpinnerModule,
	MatSelectModule,
	MatRadioModule,
	ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
