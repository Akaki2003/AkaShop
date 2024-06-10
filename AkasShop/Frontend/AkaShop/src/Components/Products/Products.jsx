import { useEffect, useState } from "react";
import axios from "axios";
import { APIURL } from "../../Constants";
import { useNavigate } from "react-router-dom";
import Loading from "../Loading";
import ReactPaginate from "react-paginate";

const Products = () => {
  const navigate = useNavigate();

  const [products, setProducts] = useState(null);
  const [loading, setLoading] = useState(false);
  const [currentPage, setCurrentPage] = useState(0);

  // Paging
  const itemsPerPage = 4;
  const [itemOffset, setItemOffset] = useState(0);

  const [url, setUrl] = useState(
    APIURL + `Products?PageSize=${itemsPerPage}&Page=${itemOffset}`
  );
  const endOffset = itemOffset + itemsPerPage;
  console.log(`Loading items from ${itemOffset} to ${endOffset}`);

  useEffect(() => {
    const fetchProducts = async () => {
      setLoading(true);
      const token = localStorage.getItem("jwt_authorization");
      const headers = {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      };
      try {
        const response = await axios.get(url, headers);
        setProducts(response.data);
        setLoading(false);
      } catch (error) {
        console.error(error);
        setLoading(false);
        if (error.response.status === 401) {
          navigate("/login");
        }
      }
    };
    fetchProducts();
  }, [itemOffset, url, navigate]);

  const pageCount = products
    ? Math.ceil(products.itemsCount / itemsPerPage)
    : 0;

  const handlePageClick = (event) => {
    const newOffset = event.selected;
    console.log(
      `User requested page number ${event.selected}, which is offset ${newOffset}`
    );
    setCurrentPage(newOffset);
    setItemOffset(newOffset);
    setUrl(APIURL + `Products?PageSize=${itemsPerPage}&Page=${newOffset}`);
  };

  if (loading || !products) return <Loading />;

  return (
    <>
      <section className="productsContainer">
        {products.items.map((product, index) => (
          <div
            className="product"
            onClick={() => navigate(`/products/${product.id}`)}
            key={index}
          >
            <div className="productText">
              <div>
                <h2 className="productTitle">{product.name}</h2>
                <p className="productDescription">{product.description}</p>
                <p className="productCreatedAt">
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
              <div>
                <p className="productPrice">{product.price}</p>
              </div>
            </div>
            <div className="productImage">
              <img src={product.imageUrl} alt="product image" />
            </div>
          </div>
        ))}
      </section>
      <div className="paginationContainer">
        <ReactPaginate
          className="pagination"
          breakLabel="..."
          nextLabel="next >"
          onPageChange={handlePageClick}
          pageRangeDisplayed={5}
          pageCount={pageCount}
          previousLabel="< previous"
          renderOnZeroPageCount={null}
          forcePage={currentPage}
        />
      </div>
    </>
  );
};

export default Products;
