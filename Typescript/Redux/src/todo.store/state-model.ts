export class TodoStateModel {
	text: string;
	conpleted: boolean;
}

export const initialState: TodoStateModel[] = [
	{
		conpleted: false,
		text: "Task 1"
	},
	{
		conpleted: false,
		text: "Task 2"
	},
	{
		conpleted: false,
		text: "Task 3"
	}
];