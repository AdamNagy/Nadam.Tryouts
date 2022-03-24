import Header from "./Header";
import Conference from "./Conference";
import Layout from "./Layout";

function App() {

  return (
    <Layout startingTheme="light">
      <div>
        <Header />
        <Conference />
      </div>
    </Layout>
  );
}

export default App;
