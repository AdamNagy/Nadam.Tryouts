export interface MithrilComponent {

	selector(state): any;
	eventActionMapping: any;
	getComponent(): any;
}