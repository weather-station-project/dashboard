import React, { Suspense } from "react";
import ReactDOM from "react-dom";
import { BrowserRouter } from "react-router-dom";
import App from "./App";
import "./i18n";
import Loading from "./components/Loading";

const BaseUrl: string = document.getElementsByTagName("base")[0].getAttribute("href") || "";
ReactDOM.render(
  <React.StrictMode>
    <BrowserRouter basename={BaseUrl}>
      <Suspense fallback={<Loading />}>
        <App />
      </Suspense>
    </BrowserRouter>
  </React.StrictMode>,
  document.getElementById("root")
);
