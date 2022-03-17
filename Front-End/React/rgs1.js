function ButtonCounter({onClickHandler, increment}) {  
	return (
    <button onClick={() => onClickHandler(increment)}>
      +{increment}
    </button>
  )
}

function Display({message}) {
  return (
    <div>{message}</div>
  )
}

function App() {
  const [counter, setCounter] = useState(0);
  const incrementers = [1,5,10,100]
  const incrementCounter = (increment) => setCounter(counter + increment );
  return (
    <div>
      <ButtonCounter onClickHandler={incrementCounter} increment={incrementers[0]}/>
      <ButtonCounter onClickHandler={incrementCounter} increment={incrementers[1]}/>
      <ButtonCounter onClickHandler={incrementCounter} increment={10}/>
      <ButtonCounter onClickHandler={incrementCounter} increment={100}/>
      <Display message={counter} />
    </div>
  )
}

ReactDOM.render(
  <App />, 
  document.getElementById('mountNode'),
);