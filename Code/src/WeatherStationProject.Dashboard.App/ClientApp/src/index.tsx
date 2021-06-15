import React, { Suspense } from "react";
import ReactDOM from "react-dom";
import { BrowserRouter } from "react-router-dom";
import App from "./App";
import Loading from "./Loading";
import "./i18n";

const baseUrl = document.getElementsByTagName("base")[0].getAttribute("href");
ReactDOM.render(
    <React.StrictMode>
        <BrowserRouter basename={baseUrl}>
            <Suspense fallback={(<Loading />)}>
                <App />
            </Suspense>
        </BrowserRouter >
  </React.StrictMode>,
  document.getElementById("root")
);

