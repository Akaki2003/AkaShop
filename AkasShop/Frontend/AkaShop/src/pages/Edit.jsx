import React, { useContext, useEffect, useState } from "react";
import {
  useLocation,
  useNavigate,
  useParams,
  useSearchParams,
} from "react-router-dom";
import { AppContext } from "../App";
import Loading from "../Components/Loading";
import { APIURL } from "../Constants";
import axios from "axios";

const Edit = () => {
  let { id } = useParams();
  const [product, setProduct] = useState(null);
  const navigate = useNavigate();
  const appContext = useContext(AppContext);
  const cookies = appContext.cookies;

  const handleEdit = () => {
    const url = APIURL + "Products";
    const headers = {
      headers: {
        Authorization: "Bearer " + cookies.get("jwt_authorization"),
      },
    };
    axios.put(url, product, headers).then((response) => {
      navigate("/");
    });
  };

  const handleChange = (event) => {
    setProduct({ ...product, [event.target.name]: event.target.value });
  };

  useEffect(() => {
    const url = APIURL + `Products/${id}`;
    axios.get(url).then((response) => {
      console.log(response);
      setProduct(response.data);
    });
  }, [id]);

  if (!product) return <Loading />;
  console.log(product);
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
              <div className="form-group">
                <label htmlFor="description">Description</label>
                <input
                  id="description"
                  name="description"
                  onChange={handleChange}
                  value={product.description}
                />
              </div>
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
