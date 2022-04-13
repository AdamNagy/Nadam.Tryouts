import React from "react";

const TodoListitem = ({todo, onRemovePressed, markTodoDone}) => (
    <div className="todo-item-container">
        <h3>{todo.text}</h3>
        <div className="buttons-container">
            <button 
                className="complete-todo"
                onClick={() => {
                    console.log(todo.text);
                    markTodoDone(todo.text);}}>Mark as completed</button>
            <button className="remove-todo" onClick={() => onRemovePressed(todo.text)}>Delete</button>
        </div>
        {todo.isCompleted ? <div>COMPLETED</div> : ""}
    </div>
);

export default TodoListitem;