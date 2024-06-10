import axios from "axios";
import React, { useContext, useRef, useState } from "react";
import { APIURL } from "../Constants";
import { AppContext } from "../App";
import { useNavigate } from "react-router-dom";

const AddProduct = () => {
  const appContext = useContext(AppContext);
  const cookies = appContext.cookies;
  const navigate = useNavigate();
  const [properties, setProperties] = useState({
    name: "",
    description: "",
    price: "",
    imageUrl: "",
  });

  const handleChnage = (event) => {
    setProperties({ ...properties, [event.target.name]: event.target.value });
  };
  const handleCreate = () => {
    const url = APIURL + "Products";
    const headers = {
      headers: {
        Authorization: "Bearer " + cookies.get("jwt_authorization"),
      },
    };
    axios.post(url, properties, headers).then((response) => {
      navigate("/");
    });
  };
  //   useRef;
  return (
    <div className="addProductPage">
      <div className="form-container">
        <form>
          <div>
            <div className="form-group">
              <label htmlFor="name">Name</label>
              <input
                type="text"
                name="name"
                value={properties.name}
                onChange={handleChnage}
              />
            </div>
            <div className="form-group">
              <label htmlFor="description">Description</label>
              <input
                type="text"
                name="description"
                value={properties.description}
                onChange={handleChnage}
              />
            </div>

            <div className="form-group">
              <label htmlFor="price">Price</label>
              <input
                type="text"
                name="price"
                value={properties.price}
                onChange={handleChnage}
              />
            </div>
            <div className="form-group">
              <label htmlFor="imageUrl">ImageUrl</label>
              <input
                type="text"
                name="imageUrl"
                value={properties.imageUrl}
                onChange={handleChnage}
              />
            </div>
          </div>
        </form>
        <button type="button" onClick={handleCreate}>
          Create
        </button>
      </div>
    </div>
  );
};

export default AddProduct;
