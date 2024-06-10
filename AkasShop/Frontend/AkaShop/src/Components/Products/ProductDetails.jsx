import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import axios from "../../api_client/axios";
import { APIURL } from "../../Constants";
import Loading from "../Loading";

const ProductDetails = () => {
  const { id } = useParams();
  const [product, setProduct] = useState(null);

  useEffect(() => {
    const url = APIURL + `Products/${id}`;
    axios.get(url).then((response) => {
      setProduct(response.data);
    });
  }, [id]);

  if (!product) return <Loading />;

  return (
    <article className="productDetails">
      <div>
        <h1 className="productTitle">{product.name}</h1>
        <img
          className="productImage"
          src={product.imageUrl}
          alt={product.name}
        />
        <p className="productDescription">{product.description}</p>
        <p className="userProductPrice">${(product.price / 100).toFixed(2)}</p>
      </div>
    </article>
  );
};

export default ProductDetails;
