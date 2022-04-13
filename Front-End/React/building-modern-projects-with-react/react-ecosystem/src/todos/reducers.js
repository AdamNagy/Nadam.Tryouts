import { CREATE_TODO, REMOVE_TODO, MARKE_DONE } from "./actions";

export const todos = (state = [], action) => {
    const {type, payload} = action;

    switch(type) {
        case CREATE_TODO: {
            const {text} = payload;
            const newTodo = {
                text,
                isCompleted: false
            }

            return state.concat(newTodo);
        }

        case REMOVE_TODO: {
            const {text} = payload;
            return state.filter(todo => todo.text !== text);
        }

        case MARKE_DONE: {
            const { text } = payload;
            const newTodos = [...state];

            for(let todo of newTodos) {
                if(todo.text === text) todo.isCompleted = true;
            }

            return newTodos;
        }
        default:
            return state;
    }
}