import React from "react";
import TodoListItem from "./TodoListItem.js";
import NewTodoForm from "./NewTodoForm.js";
import { connect } from "react-redux";
import { removeTodo, markTodoDone } from "./actions"

const TodoList = ({todos = [], onRemovePressed, onCompletePressed}) => (
    <div className="list-wrapper">
        <NewTodoForm />
        {todos.map((todo) => <TodoListItem todo={todo} onRemovePressed={onRemovePressed} markTodoDone={onCompletePressed}></TodoListItem>)}
    </div>
);

const mapStateToProps = (state) => ({
    todos: state.todos
})

const mapDispatchToProps = (dispatch) => ({
    onRemovePressed: text => dispatch(removeTodo(text)),
    onCompletePressed: text => dispatch(markTodoDone(text))
})

export default connect(mapStateToProps, mapDispatchToProps)(TodoList);
