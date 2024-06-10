import React, { useCallback, useContext } from "react";
import Products from "../Components/Products/Products";

const Home = () => {
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
