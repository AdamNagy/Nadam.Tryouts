import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Note } from '../note.model';
import * as _ from "lodash";

@Injectable({
	providedIn: 'root'
})
export class NoteService {

	private notes: Note[];

	constructor() {
		this.notes = [];
		this.notes.push({
			owner: "2",
			text: "lorem ipsum"
		});

		this.notes.push({
			owner: "2",
			text: "dolor sit"
		});

		this.notes.push({
			owner: "2",
			text: "amet sdfgaj"
		});
	 }


	public Get(userId): Observable<string[]> {

		return of(_.filter(this.notes, (item) => item.owner == userId));
	}

}
