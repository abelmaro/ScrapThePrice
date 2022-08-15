import React, { useEffect, useRef, useState } from "react";
import { motion } from "framer-motion";
import "./Slider.css";

const Slider = (props) => {
  const [width, setWidth] = useState(0);
  const carousel = useRef();

  useEffect(() => {
    setWidth(carousel.current.scrollWidth - carousel.current.offsetWidth);
  }, []);

  return (
    <motion.div ref={carousel} className="carousel">
      <motion.div
        drag="x"
        dragConstraints={{ right: 0, left: -width }}
        whileTap={{ cursor: "grabbing" }}
        className="inner-carousel"
      >
        {
        props.prods.map((product) => {
          return (
            <motion.div
              exit={{ opacity: 0 }}
              initial={{ opacity: 0 }}
              animate={{ opacity: 1 }}
              className="item"
            >
              <div>
                <img src={product.imageUrl} alt="" />
              </div>
              <div>
                {product.name} - ${product.price}
              </div>
              <button onClick={() => window.open(product.productUrl)}>
                Ver producto
              </button>
            </motion.div>
          );
        })}
      </motion.div>
    </motion.div>
  );
};

export default Slider;
