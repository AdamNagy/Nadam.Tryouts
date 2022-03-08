import React, { useEffect, useReducer, useState } from 'react';
import ReactDOM from 'react-dom';
import './index.css';

const config = {
  htmlClass: 'heading',
  name: 'Adam'
}

function People({people}) {
  return (
    <ul>
      {people.map((person) => (<li key={person.key}>{person.name} : {person.age}</li>))}
    </ul>
  )
}

function App2() {

  const people = [
    {key: 1, name: "Adam", age: 33},
    {key: 2, name: "Fanni", age: 31}
  ]

  const [status, setStatus] = useState("open");
  const [manager, setManager] = useState("Alex");

  return (
    <>
      <h2>manager in duti: {manager}</h2>
      <button onClick={() => setManager("Rachel")}></button>
      <div className="App">
        <header className="App-header">
            Learn React!!!
            Hello word
        </header>
        <h1>Status {status} </h1>

        <button onClick={() => setStatus("closed")}> Close </button>
        <button onClick={() => setStatus("open")}> Open </button>
      </div>
      <People people={people}> </People>
    </>
  );
}

function Checkbox() {

  const [checked, toggle] = useReducer(checked => !checked, false);

  const [val, setVal] = useState("");
  const [val2, setVal2] = useState("");

  useEffect(() => {
    console.log(checked);
  }, [checked]);

  useEffect(() => {
    console.log(`val ${val}`);
  }, [val]);

  useEffect(() => {
    console.log(`val2 ${val2}`);
  }, [val2]);

  return (
    <>
      <input type="checkbox" value={checked} onChange={toggle}></input>
      {checked ? "checked" : "Not checked"}
      <br></br>
      <br></br><br></br><br></br>
      <label>Favourite phrase</label><br />
      <input type="text" value={val} onChange={e => setVal(e.target.value) }></input><br />
      <label>Second Favourite phrase</label><br />
      <input type="text" value={val2} onChange={e => setVal2(e.target.value) }></input><br />
    </>
  )
}

function WenContent({url}) {
  const [data, setData] = useState(null);

  useEffect(() => {
    fetch(url).then((response) => {
      console.log(response);
      setData(response.url);
    }).catch((error) => console.log(error))
  }, []);

  if( data ) {
    return (
      <>
        <h1>Called url:</h1>
        <div>{data}</div>
      </>
    )
  }

  return <h1>No Data</h1>
}

ReactDOM.render(
  <>
    <Checkbox></Checkbox>
    <WenContent url="https://api.github.com/users/AdamNagy"></WenContent>
  </>,
  document.getElementById('root')
);
