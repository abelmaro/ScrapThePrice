import logo from "./logo.svg";
import "./App.css";
import Slider from "./Components/Slider/Slider";
import { useEffect, useState } from "react";
import { motion } from "framer-motion";

function App() {
  const [productsML, setProductsML] = useState([]);
  const [productsOLX, setProductsOLX] = useState([]);
  const [productsFravega, setProductsFravega] = useState([]);
  const [products, setProducts] = useState([]);
  const [searchText, setSearchText] = useState("Termo Stanley");
  const [priceRange, setPriceRange] = useState(0);

  const data = [
    {
      key: "MERCADOLIBRE",
      value: [
        {
          name: "Manija Repuesto Para Termo Stanley Explorer Outdoors",
          url: null,
          imageUrl:
            "https://http2.mlstatic.com/D_NQ_NP_656277-MLA49514880404_032022-V.webp",
          productUrl:
            "https://articulo.mercadolibre.com.ar/MLA-1129901663-manija-repuesto-para-termo-stanley-explorer-outdoors-_JM?searchVariation=174355986640#searchVariation=174355986640&position=5&search_layout=stack&type=item&tracking_id=842a37fe-8763-493f-87a0-df45b04bcb84",
          price: 1500,
          site: "MERCADOLIBRE",
        },
        {
          name: "Bolso Matero Portatermo Stanley Waterdog Coleman Camuflado",
          url: null,
          imageUrl:
            "https://http2.mlstatic.com/D_NQ_NP_678442-MLA50504622180_062022-V.webp",
          productUrl:
            "https://articulo.mercadolibre.com.ar/MLA-1144739329-bolso-matero-portatermo-stanley-waterdog-coleman-camuflado-_JM?searchVariation=174726268971#searchVariation=174726268971&position=7&search_layout=stack&type=item&tracking_id=842a37fe-8763-493f-87a0-df45b04bcb84",
          price: 3754,
          site: "MERCADOLIBRE",
        },
        {
          name: "Bolso Matero Qusto Big Doble Apto Termo Stanley 1lt Y 1.3lt",
          url: null,
          imageUrl:
            "https://http2.mlstatic.com/D_NQ_NP_886166-MLA32635512970_102019-V.webp",
          productUrl:
            "https://articulo.mercadolibre.com.ar/MLA-715836230-bolso-matero-qusto-big-doble-apto-termo-stanley-1lt-y-13lt-_JM?searchVariation=45296344928#searchVariation=45296344928&position=8&search_layout=stack&type=item&tracking_id=842a37fe-8763-493f-87a0-df45b04bcb84",
          price: 4110,
          site: "MERCADOLIBRE",
        },
        {
          name: "Termo Stanley Classic Legendary Bottle 1.5 QT de acero inoxidable matte black",
          url: null,
          imageUrl:
            "https://http2.mlstatic.com/D_NQ_NP_631541-MLA46106476522_052021-V.webp",
          productUrl:
            "https://www.mercadolibre.com.ar/termo-stanley-classic-legendary-bottle-15-qt-de-acero-inoxidable-matte-black/p/MLA15542559?pdp_filters=category:MLA47769%7CITEM_CONDITION:2230284#searchVariation=MLA15542559&position=49&search_layout=stack&type=product&tracking_id=842a37fe-8763-493f-87a0-df45b04bcb84",
          price: 18000,
          site: "MERCADOLIBRE",
        },
        {
          name: "Termo Stanley 1.3 Lts Original Acero Inoxidable",
          url: null,
          imageUrl:
            "https://http2.mlstatic.com/D_NQ_NP_766860-MLA51116472733_082022-V.webp",
          productUrl:
            "https://articulo.mercadolibre.com.ar/MLA-1147450549-termo-stanley-13-lts-original-acero-inoxidable-_JM?searchVariation=174845952353#searchVariation=174845952353&position=42&search_layout=stack&type=item&tracking_id=842a37fe-8763-493f-87a0-df45b04bcb84",
          price: 18780,
          site: "MERCADOLIBRE",
        },
      ],
    },
    {
      key: "FRAVEGA",
      value: [
        {
          name: "Termo Stanley Classic 1 Litro C/manija 24hs Frio/calor",
          url: null,
          imageUrl:
            "https://images.fravega.com/f300/a859418890f5d1fa929101d3e4c0c48f.jpg.webp",
          productUrl:
            "https://www.fravega.com/p/termo-stanley-classic-1-litro-c-manija-24hs-frio-calor-50033297/",
          price: 27998,
          site: "FRAVEGA",
        },
        {
          name: "Termo Stanley 950mt Pico Cebador",
          url: null,
          imageUrl:
            "https://images.fravega.com/f300/1dfca103bb90035151c57151e037518e.jpg.webp",
          productUrl:
            "https://www.fravega.com/p/termo-stanley-950mt-pico-cebador-20045061/",
          price: 29199,
          site: "FRAVEGA",
        },
        {
          name: "Termo Stanley Clasico - 950 Ml Rojo Manija y Tapon Cebador (10-10148-003)",
          url: null,
          imageUrl:
            "https://images.fravega.com/f300/560147e1669fc4c495ca96b66e718bf6.jpg.webp",
          productUrl:
            "https://www.fravega.com/p/termo-stanley-clasico-950-ml-rojo-manija-y-tapon-cebador-(10-10148-003)-20033212/",
          price: 29199,
          site: "FRAVEGA",
        },
        {
          name: "Termo Stanley Clasico - 950 Ml Nightfall Manija y Tapon Cebador (10-10148-006)",
          url: null,
          imageUrl:
            "https://images.fravega.com/f300/17243658e191fb3d41dcf7a6785f3b82.jpg.webp",
          productUrl:
            "https://www.fravega.com/p/termo-stanley-clasico-950-ml-nightfall-manija-y-tapon-cebador-(10-10148-006)-20033213/",
          price: 29199,
          site: "FRAVEGA",
        },
        {
          name: "Termo Stanley Clasico - 950 Ml Verde Manija y Tapon Cebador (10-10148-001)",
          url: null,
          imageUrl:
            "https://images.fravega.com/f300/2680039839de858f444ecc0b12ae0c4b.jpg.webp",
          productUrl:
            "https://www.fravega.com/p/termo-stanley-clasico-950-ml-verde-manija-y-tapon-cebador-(10-10148-001)-20033214/",
          price: 29199,
          site: "FRAVEGA",
        },
      ],
    },
  ];

  const searchProducts = () => {
    getProducts();
  };

  useEffect(()=>{
    var filteredData = filterByPrice(data)
    setProducts(filteredData);
  },[priceRange]);

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

  const filterByPrice = (filteredData) => {
    const filteredProducts = []
    // filteredData.map(x => (x.key, x.value.filter((pr) => pr.price > priceRange)));
    filteredData.forEach((v, i) => {
      var keyName = v.key;
      var values = v.value.filter((pr) => pr.price > priceRange);
      filteredProducts.push({key: keyName, value: values})
    });

    return filteredProducts;
  };

  const filterEvenResults = (pricec) =>
    setProducts((items) => items.filter((x) => x.price > pricec));

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
          <input
            className="price-range-filter"
            type="range"
            min={100}
            max={100000}
            onChange={(x) => {
              setPriceRange(x.target.value)
              filterEvenResults(x.target.value);
            }}
            value={priceRange}
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
              <img src={brandImg} alt="" />

              {<Slider prods={x.value} />}
            </motion.div>
          );
        })}
      </header>
    </div>
  );
}

export default App;
