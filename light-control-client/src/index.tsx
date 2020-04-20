import * as React from "react";
import ReactDOM from "react-dom";
import "./index.css";
import { Strand } from "./components/strand";

function App() {
  return (
    <div>
      <header>
        <h1>Light Control!</h1>
      </header>
      <body>
        <div className="strand-manager">
          <Strand />
        </div>
      </body>
    </div>
  );
}

ReactDOM.render(<App />, document.getElementById("root"));
