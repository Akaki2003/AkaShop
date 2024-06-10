import "./App.css";
import Navbar from "./Components/Navbar/Navbar";
import { AuthProvider } from "./context/AuthContext";
import RoutesList from "./pages/PageRoutes/RoutesList";

function App() {
  // return <Products />;
  return (
    <AuthProvider>
      <>
        <header>
          <Navbar />
        </header>
        <main className="container">
          <RoutesList />
        </main>
        <footer></footer>
      </>
    </AuthProvider>
  );
}

export default App;
