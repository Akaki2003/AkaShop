import axios from "axios";
import React, { useContext, useState } from "react";
import { APIURL } from "../Constants";
import { useNavigate } from "react-router-dom";

const AddProduct = () => {
  const navigate = useNavigate();
  const [properties, setProperties] = useState({
    name: "",
    description: "",
    price: "",
    imageUrl: "",
  });

  const handleChange = (event) => {
    setProperties({ ...properties, [event.target.name]: event.target.value });
  };

  const handleCreate = () => {
    const url = APIURL + "Products";
    const token = localStorage.getItem("jwt_authorization");
    const headers = {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    };
    axios
      .post(url, properties, headers)
      .then((response) => {
        navigate("/");
      })
      .catch((error) => {
        console.error("Error creating product:", error);
        if (error.response.status === 401) {
          navigate("/login");
        }
      });
  };

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
                onChange={handleChange}
              />
            </div>
            <div className="form-group">
              <label htmlFor="description">Description</label>
              <input
                type="text"
                name="description"
                value={properties.description}
                onChange={handleChange}
              />
            </div>

            <div className="form-group">
              <label htmlFor="price">Price</label>
              <input
                type="text"
                name="price"
                value={properties.price}
                onChange={handleChange}
              />
            </div>
            <div className="form-group">
              <label htmlFor="imageUrl">ImageUrl</label>
              <input
                type="text"
                name="imageUrl"
                value={properties.imageUrl}
                onChange={handleChange}
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
