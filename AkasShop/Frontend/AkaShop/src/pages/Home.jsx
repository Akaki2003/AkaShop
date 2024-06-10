import React, { useCallback, useContext } from "react";
import Products from "../Components/Products/Products";
import { AppContext } from "../App";

const Home = () => {
  const appContext = useContext(AppContext);
  return (
    <section className="productsPage">
      <section className="addProduct">
        <a href="/addProduct">Add Product</a>
      </section>
      <Products />
    </section>
  );
};

export default Home;
