import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'tinymce-angular-demo';
  tinyMceInitConfig = {
	  height: 500,
	  menubar: true,
	  plugins: [
		'advlist autolink lists link image charmap print preview anchor',
		'searchreplace visualblocks code fullscreen',
		'insertdatetime media table paste code help wordcount'
	  ],
	  toolbar:
		'undo redo | formatselect | bold italic backcolor | \
		alignleft aligncenter alignright alignjustify | \
		bullist numlist outdent indent | removeformat | help'
  };
  content: string;
  initialContent = '<p>This is the initial content of the editor</p>';

  private getContent(event): void {
	console.log(event.editor.getContent());
  }
}

