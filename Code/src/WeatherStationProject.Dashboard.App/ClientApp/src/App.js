import React from 'react';
import { Route } from 'react-router';
import { Layout } from './components/menu/Layout';
import Home from './components/pages/Home';
import CurrentData from './components/pages/CurrentData/CurrentData';
import HistoricalData from './components/pages/HistoricalData';
import MeasurementsList from './components/pages/MeasurementsList';

function App() {
    return (
        <Layout>
            <Route exact path='/' component={Home} />
            <Route path='/currentdata' component={CurrentData} />
            <Route path='/historicaldata' component={HistoricalData} />
            <Route path='/measurementslist' component={MeasurementsList} />
        </Layout>
    );
}

export default App;
