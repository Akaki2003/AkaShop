import React, { useContext, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import Loading from "../Components/Loading";
import { APIURL } from "../Constants";
import axios from "axios";

const Edit = () => {
  let { id } = useParams();
  const [product, setProduct] = useState(null);
  const navigate = useNavigate();

  const handleEdit = () => {
    const url = APIURL + "Products";
    const token = localStorage.getItem("jwt_authorization");
    const headers = {
      headers: {
        Authorization: "Bearer " + token,
      },
    };
    axios
      .put(url, product, headers)
      .then((response) => {
        navigate("/");
      })
      .catch((error) => {
        console.error("Error updating product:", error);
        if (error.response.status === 401) {
          navigate("/login");
        }
      });
  };

  const handleChange = (event) => {
    setProduct({ ...product, [event.target.name]: event.target.value });
  };

  useEffect(() => {
    const url = APIURL + `Products/${id}`;
    axios
      .get(url)
      .then((response) => {
        setProduct(response.data);
      })
      .catch((error) => {
        console.error("Error fetching product:", error);
        if (error.response.status === 401) {
          navigate("/login");
        }
      });
  }, [id, navigate]);

  if (!product) return <Loading />;

  return (
    <div className="editPage">
      <article className="form-container">
        <form>
          <div>
            <div className="form-group">
              <label htmlFor="name">Title</label>
              <input
                id="name"
                name="name"
                onChange={handleChange}
                value={product.name}
              />
            </div>

            <div className="form-group">
              <label htmlFor="imageUrl">Image URL</label>
              <input
                id="imageUrl"
                name="imageUrl"
                onChange={handleChange}
                value={product.imageUrl}
              />
            </div>
            <div className="form-group">
              <label htmlFor="description">Description</label>
              <input
                id="description"
                name="description"
                onChange={handleChange}
                value={product.description}
              />
            </div>
            <div className="form-group">
              <label htmlFor="price">Price</label>
              <input
                id="price"
                name="price"
                onChange={handleChange}
                value={product.price}
              />
            </div>
          </div>
        </form>
        <button type="button" onClick={handleEdit}>
          Save
        </button>
      </article>
    </div>
  );
};

export default Edit;
