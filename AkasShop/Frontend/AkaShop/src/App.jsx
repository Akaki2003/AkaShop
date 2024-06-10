import "./App.css";
import Navbar from "./Components/Navbar/Navbar";
import Pricing from "./pages/Pricing";
import About from "./pages/About";
import Home from "./pages/Home";
import { Route, Routes } from "react-router-dom";
import AddProduct from "./pages/AddProduct";
import Login from "./pages/Auth/Login";
import { createContext } from "react";
import Cookies from "universal-cookie";
import { AuthProvider } from "./context/AuthContext";
import Profile from "./pages/Profile";
import ProductDetails from "./Components/Products/ProductDetails";
// import PrivateRoute from "./Components/PrivateComponent";
import RoutesList from "./pages/PageRoutes/RoutesList";

export const AppContext = createContext();

function App() {
  // return <Products />;
  return (
    <AppContext.Provider value={{ cookies: new Cookies() }}>
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
    </AppContext.Provider>
  );
}

export default App;
