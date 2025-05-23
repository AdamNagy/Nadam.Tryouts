import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { EditorModule } from '@tinymce/tinymce-angular';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
	BrowserModule,
	EditorModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
