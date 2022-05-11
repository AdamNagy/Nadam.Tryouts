
export const CREATE_TODO = "CREATE_TODO";
export const createTodo = (text) => ({
    type: CREATE_TODO,
    payload: {text}
});

export const REMOVE_TODO = "REMOVE_TODO";
export const removeTodo = (text) => ({
    type: REMOVE_TODO,
    payload: {text}
});

export const MARKE_DONE = "MARKE_DONE";
export const markTodoDone = (text) => ({
    type: MARKE_DONE,
    payload: { text }
});

export const LOAD_TODOS_IN_PROGRESS = "LOAD_TODOS_IN_PROGRESS";
export const loadTodosInProgress = () => ({
    type: LOAD_TODOS_IN_PROGRESS
})

export const LOAD_TODOS_SUCCESS = "LOAD_TODOS_SUCCESS";
export const loadTodosSUCCESS = (todos) => ({
    type: LOAD_TODOS_SUCCESS,
    payload: {todos}
})

export const LOAD_TODOS_FAILED = "LOAD_TODOS_FAILED";
export const loadTodosFailed = (todos) => ({
    type: LOAD_TODOS_FAILED,
})
