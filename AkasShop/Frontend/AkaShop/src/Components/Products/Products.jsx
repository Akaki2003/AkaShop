import { useEffect, useState, useContext } from "react";
import axios from "axios";
import { APIURL } from "../../Constants";
import { AppContext } from "../../App";
import { useNavigate } from "react-router-dom";
import Loading from "../Loading";
import ReactPaginate from "react-paginate";

const Products = () => {
  const navigate = useNavigate();
  const appContext = useContext(AppContext);
  const cookies = appContext.cookies;

  const [products, setProducts] = useState(null);

  //paging
  const itemsPerPage = 4;
  const [itemOffset, setItemOffset] = useState(0);

  const [url, setUrl] = useState(
    APIURL + `Products?PageSize=${itemsPerPage}&Page=${itemOffset}`
  );
  const endOffset = itemOffset + itemsPerPage;
  console.log(`Loading items from ${itemOffset} to ${endOffset}`);

  useEffect(() => {
    const headers = {
      headers: {
        Authorization: "Bearer " + cookies.get("jwt_authorization"),
      },
    };
    axios.get(url, headers).then((response) => {
      setProducts(response.data);
    });
  }, [itemOffset]);
  const pageCount = products
    ? Math.ceil(products.itemsCount / itemsPerPage)
    : 0;

  // Invoke when user click to request another page.
  const handlePageClick = (event) => {
    // const newOffset = (event.selected * itemsPerPage) % products.itemsCount;
    const newOffset = event.selected;
    console.log(
      `User requested page number ${event.selected}, which is offset ${newOffset}`
    );
    setItemOffset(newOffset);
    setUrl(APIURL + `Products?PageSize=${itemsPerPage}&Page=${newOffset}`);
  };
  //paging

  if (!products) return <Loading />;

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
                <h2 className="productDescription">
                  description: {product.description}
                </h2>
                <h2 className="productCreatedAt">
                  {new Date(product.createdAt).toLocaleDateString("en-CA")}
                </h2>
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
        />
      </div>
    </>
  );
};

export default Products;
