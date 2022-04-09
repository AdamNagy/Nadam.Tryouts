import logo from './logo.svg';
import './App.css';
import { StarRating } from './star-rating/star-rating';
import { useEffect, useState } from 'react';

function App() {
  const [name, setName] = useState("Jen");
  useEffect(() => {
    document.title = `Celebrate ${name}`
  }, [name]);

  return (
    <div className="App">
      <h1>Hello word</h1>
        <StarRating totalStars={10}/>
        <section>
          <p>Congrats {name}</p>
          <button onClick={() => setName("Gemma")}
          >Change winner</button>
        </section>
    </div>
  );
}

export default App;
