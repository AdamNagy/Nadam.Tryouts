import React, {useState} from "react";
import { connect } from "react-redux";
import { createTodo } from "./actions";

const NewTodoForm = ({todos, onCreatePressed}) => {
    const [inputValue, setInputValue] = useState("");

    return (
        <div className="new-todo-form">
            <input type="text" 
                className="new-todo-formm"
                value={inputValue} 
                onChange={e => setInputValue(e.target.value)}
                placeholder="Tye new todo.."></input>
            <button className="new-todo-button"
                onClick={() => {
                    onCreatePressed(inputValue); 
                    setInputValue("")
                }}>
                    Create Todo
            </button>
        </div>
);}

// the whole state return the slice (selector??)
const mapStateToProps = state => ({
    todos: state.todos
});

const mapDipatchToProps = dispatch => ({
    onCreatePressed: text => dispatch(createTodo(text))
});

export default connect(mapStateToProps, mapDipatchToProps)(NewTodoForm);
