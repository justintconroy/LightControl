import * as React from "react";
import ReactDOM from "react-dom";
import { Light } from "./components/light";

function App() {
  return (
    <div>
      <header>
        <h1>Light Control!</h1>
      </header>
      <body>
        <div>
          <Light />
        </div>
      </body>
    </div>
  );
}

ReactDOM.render(<App />, document.getElementById("root"));
