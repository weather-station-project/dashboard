import React from "react";
import { Route } from "react-router";
import Layout from "./components/menu/Layout";
import Home from "./components/pages/Home";

const App: React.FC = () => {
    return (
        <Layout>
            <Route exact path="/" component={Home} />
            {/*<Route path="/currentdata" component={CurrentData} />
            <Route path="/historicaldata" component={HistoricalData} />
            <Route path="/measurementslist" component={MeasurementsList} />*/}
        </Layout>
    );
};

export default App;
