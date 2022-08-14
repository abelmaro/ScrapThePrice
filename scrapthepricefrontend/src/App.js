import logo from "./logo.svg";
import "./App.css";
import Slider from "./Components/Slider/Slider";
import { useState } from "react";
import { motion } from "framer-motion";

function App() {
  const [productsML, setProductsML] = useState([]);
  const [productsOLX, setProductsOLX] = useState([]);
  const [productsFravega, setProductsFravega] = useState([]);
  const [products, setProducts] = useState([]);
  const [searchText, setSearchText] = useState("iPhone 13 Pro Max");

  const searchProducts = () => {
    getOLXProducts();
    getMLProducts();
    getFravegaProducts();
  };

  function getMLProducts() {
    var url =
      "https://localhost:7296/api/GetProductFromML?productName=" + searchText;
    fetch(url).then((res) => {
      res.json().then((p) => {
        setProductsML(p);
        console.log(p);
      });
    });
  }

  function getOLXProducts() {
    var url =
      "https://localhost:7296/api/GetProductFromOLX?productName=" + searchText;
    fetch(url).then((res) => {
      res.json().then((p) => {
        setProductsOLX(p);
        console.log(p);
      });
    });
  }

  function getFravegaProducts() {
    var url =
      "https://localhost:7296/api/GetProductFromFravega?productName=" +
      searchText;
    fetch(url).then((res) => {
      res.json().then((p) => {
        setProductsFravega(p);
      });
    });
  }

  async function getFravegaProducts() {
    var url =
      "https://localhost:7296/api/GetProducts?productName=" + searchText;
    fetch(url).then((res) => {
      res.json().then((p) => {
        setProducts(p);
      });
    });
  }

  return (
    <div className="App">
      <header className="App-header">
        <div className="search-container">
          <input
            className="search-bar"
            type="text"
            onChange={(x) => setSearchText(x.target.value)}
            value={searchText}
          ></input>
          <button onClick={searchProducts}>Search</button>
        </div>
        {productsML.length > 0 ? (
          <motion.div
            transition={{ ease: "easeOut", duration: 1, bounce: true }}
            className="products-container ml-p"
          >
            <img src="https://http2.mlstatic.com/static/org-img/homesnw/mercado-libre.png?v=2" />

            {<Slider prods={productsML} />}
          </motion.div>
        ) : (
          <></>
        )}

        {productsOLX.length > 0 ? (
          <div className="products-container olx-p">
            <img src="https://is1-ssl.mzstatic.com/image/thumb/Purple113/v4/47/1a/a1/471aa1b0-ea40-2e9b-1459-46a7fab6de8a/source/200x200bb.jpg" />
            {<Slider prods={productsOLX} />}
          </div>
        ) : (
          <></>
        )}
        {productsFravega.length > 0 ? (
          <div className="products-container fravega-p">
            <img src="https://static01.eu/catalogosyofertas.com.ar/images/store/3.png" />
            {<Slider prods={productsFravega} />}
          </div>
        ) : (
          <></>
        )}
      </header>
    </div>
  );
}

export default App;
