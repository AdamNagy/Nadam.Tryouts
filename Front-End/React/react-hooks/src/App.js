import logo from './logo.svg';
import { CheckBox } from './components/check-box'
import { useInput } from './components/useInput';
import './App.css';
import { StarRating } from './components/star-rating';
import { useEffect, useState, useReducer, useRef, createContext, useContext } from 'react';
import { TreesContext} from './index';


const initialState = {
  checked: false,
  message: "hi"
}

const reducer = (state, action) => {
  switch(action.type) {
    case 'yell':
      return {
        message: `HEY!! ${state.message}`
      };

    case 'whisper': return {
      message: 'excuse me'
    }
  }
}

function App() {
  const [name, setName] = useState("Jen");
  
  const [number, setNumber] = useReducer(
    (number, newNumber) => number + newNumber,
    0
  );

  useEffect(() => {
    document.title = `Celebrate ${name}`;
  }, [name]);

  const [state, dispatch] = useReducer(
    reducer,
    initialState
  );

  const sound = useRef();
  const color = useRef();

  const submit = (event) => {
    event.preventDefault();
    const soundVal = sound.current.value;
    const colorVal = color.current.value;

    alert(`${soundVal} ${colorVal}`);
  }

  const [dayState, resetDay] = useInput("Monday");

  const trees = useContext(TreesContext);

  return (       
    <div className="App">
      <h1>Hello word</h1>
        <StarRating totalStars={10}/>
        <section>
          <p>Congrats {name}</p>
          <button onClick={() => setName("Gemma")}
          >Change winner</button>
        </section>
        <h1 onClick={() => setNumber(1)}>{number}</h1>
        <CheckBox></CheckBox>
        <p>{state.message}</p>
        <button onClick={() => dispatch({type: 'yell'})}>YELL</button>
        <button onClick={() => dispatch({type: 'whisper'})}>YELL</button>
        <form onSubmit={submit}>
          <input ref={sound} type="text" placeholder='sound..'></input>
          <input ref={color} type="color" ></input>
          <button>Add</button>
        </form>

        <h2>{dayState.value}</h2>
        <input type="text" placeholder='day..' value={dayState.value} onChange={dayState.onChange}></input>
        <button onClick={resetDay}>RESET</button>

        <ul>
          {trees.map((tree) => (<li key={tree.id}>{tree.type}</li>))}
        </ul>
    </div>
  );
}

export default App;
