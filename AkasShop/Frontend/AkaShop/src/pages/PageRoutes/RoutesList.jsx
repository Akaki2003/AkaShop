import React from "react";
import { Route, Routes } from "react-router-dom";
import Home from "../Home";
import Profile from "../Profile";
import Pricing from "../Pricing";
import About from "../About";
import AddProduct from "../AddProduct";
import Login from "../Auth/Login";
import ProductDetails from "../../Components/Products/ProductDetails";
import PrivateRoute from "../../Components/PrivateComponent";
import Edit from "../Edit";
import Register from "../Auth/Register";

const RoutesList = () => {
  return (
    <Routes>
      <Route path={("/home", "/")} element={<Home />} />
      <Route
        path="/profile"
        element={
          <PrivateRoute>
            <Profile />
          </PrivateRoute>
        }
      />
      <Route path="/pricing" element={<Pricing />} />
      <Route path="/about" element={<About />} />
      <Route
        path="/addProduct"
        element={
          <PrivateRoute>
            <AddProduct />
          </PrivateRoute>
        }
      />
      <Route path="/login" element={<Login />} />
      <Route path="/register" element={<Register />} />
      <Route path="/products/:id" element={<ProductDetails />} />
      <Route
        path="/edit/:id"
        element={
          <PrivateRoute>
            <Edit />
          </PrivateRoute>
        }
      />
    </Routes>
  );
};

export default RoutesList;
