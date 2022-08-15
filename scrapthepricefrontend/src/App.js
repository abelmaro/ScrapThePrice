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
  const [searchText, setSearchText] = useState("Termo Stanley");

  const searchProducts = () => {
    getProducts();
  };

  async function getProducts() {
    var url =
      "https://localhost:7296/api/GetProducts?productName=" + searchText;
    fetch(url).then((res) => {
      res.json().then((p) => {
        console.log(p);
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
          <button onClick={searchProducts}>Buscar</button>
        </div>

        {products.map((x) => {
          var brandImg = "";
          switch (x.key) {
            case "MERCADOLIBRE":
              brandImg =
                "https://http2.mlstatic.com/static/org-img/homesnw/mercado-libre.png?v=2";
              break;
            case "OLX":
              brandImg =
                "https://is1-ssl.mzstatic.com/image/thumb/Purple113/v4/47/1a/a1/471aa1b0-ea40-2e9b-1459-46a7fab6de8a/source/200x200bb.jpg";
              break;
            case "FRAVEGA":
              brandImg =
                "https://static01.eu/catalogosyofertas.com.ar/images/store/3.png";
              break;
            default:
              break;
          }

          return (
            <motion.div
              transition={{ ease: "easeOut", duration: 1, bounce: true }}
              className="products-container"
            >
              <img src={brandImg} />

              {<Slider prods={x.value} />}
            </motion.div>
          );
        })}
      </header>
    </div>
  );
}

export default App;
