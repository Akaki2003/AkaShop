import React, { useContext, useEffect, useState } from "react";
import axios from "axios";
import { APIURL } from "../Constants";
import Tabs from "../Components/Tabs/Tabs";
import { useNavigate } from "react-router-dom";

const Profile = () => {
  const navigate = useNavigate();
  const [profile, setProfile] = useState(null);
  const [products, setProducts] = useState(null);

  useEffect(() => {
    const fetchProfile = async () => {
      try {
        const token = localStorage.getItem("jwt_authorization");
        const url = `${APIURL}User`;
        const headers = {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        };
        const response = await axios.get(url, headers);
        setProfile(response.data);
      } catch (error) {
        console.error("Error fetching profile:", error);
        if (error.response.status === 401) {
          navigate("/login");
        }
      }
    };

    fetchProfile();
  }, [navigate]);

  useEffect(() => {
    const getUserProducts = async () => {
      try {
        const token = localStorage.getItem("jwt_authorization");
        const url = `${APIURL}Products/UserProducts`;
        const headers = {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        };
        const response = await axios.get(url, headers);
        setProducts(response.data);
      } catch (error) {
        console.error("Error getting user products:", error);
        if (error.response.status === 401) {
          navigate("/login");
        }
      }
    };

    getUserProducts();
  }, [navigate]);

  if (!profile || !products) {
    return <p>Loading...</p>;
  }

  const tabData = [{ label: "Profile" }, { label: "User Products" }];

  const { id, email, productCount } = profile;

  const handleAddProduct = () => {
    navigate("/addProduct");
  };

  return (
    <section className="profileContainer">
      <Tabs tabs={tabData}>
        <section className="profile">
          <h2>Id: {id}</h2>
          <h2>Email: {email}</h2>
          <h2>Product count: {productCount}</h2>
        </section>
        <section className="user-products">
          {products.items.length ? (
            products.items.map((product) => (
              <div
                key={product.id}
                className="userProduct"
                onClick={() => navigate(`/edit/${product.id}`)}
              >
                <div className="userProductDetails">
                  <div>
                    <h3 className="userProductDetailsTitle">{product.name}</h3>
                    <p>{product.description}</p>
                    <p>
                      created at:{" "}
                      {new Date(product.createdAt).toLocaleDateString("en-GB", {
                        day: "numeric",
                        month: "short",
                      })}{" "}
                      /{" "}
                      {new Date(product.createdAt).toLocaleTimeString("en-GB", {
                        hour: "2-digit",
                        minute: "2-digit",
                      })}
                    </p>
                  </div>
                  <p className="userProductPrice">${product.price}</p>
                </div>
                <div className="userProductImg">
                  <img
                    src={product.imageUrl}
                    alt={product.name}
                    width="300px"
                  />
                </div>
              </div>
            ))
          ) : (
            <div className="no-products">
              <h3>You have no products yet.</h3>
              <button className="add-product-btn" onClick={handleAddProduct}>
                Add Product
              </button>
            </div>
          )}
        </section>
      </Tabs>
    </section>
  );
};

export default Profile;
