import {useState} from "react";

import Header from "./Header";
import Conference from "./Conference";

function App() {

  const [theme, setTheme] = useState("dark");

  return (
    <div className={theme === "light" ? "container-fluid light" : "container-fluid dark"} >
      <Header theme={theme}/>
      <Conference
        theme={theme} 
        setTheme={setTheme} />
    </div>
  );
}

export default App;
