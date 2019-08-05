export enum GameType {
	addition, subtraction, multiplication, dates
} 

export interface GameState {

	level: number;
	type: GameType;
	left: string;
	right: string;
	solution: string;
}