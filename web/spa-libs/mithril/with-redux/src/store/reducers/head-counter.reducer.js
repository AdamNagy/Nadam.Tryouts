const tasksReducer = (state = [], action) => {
	switch (action.type) {
		case 'ADD_TASK':
			
			return [
				...state,
				action.payload
			]
	
		default:
			break;
	}
}

export default tasksReducer;