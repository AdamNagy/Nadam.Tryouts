import { loadTodosFailed, loadTodosInProgress, loadTodosSUCCESS } from "./actions";

export const loadTodos = () => async (dispatch, getState) => {
    try {
        dispatch(loadTodosInProgress);
        const response = await fetch("http://localhost:8080/todos");
        const todos = await response.json();
        dispatch(loadTodosSUCCESS(todos));
    } catch(e) {
        dispatch(loadTodosFailed());
        dispatch(displayAlert(e))
    }
}

export const displayAlert = (text) => () => {
    alert(text);
}
