import React from 'react';
import {Route, Routes} from 'react-router-dom';
import Layout from './components/menu/Layout';
import Home from './components/pages/Home';
import CurrentData from './components/pages/CurrentData/CurrentData';
import HistoricalData from './components/pages/HistoricalData';
import MeasurementsList from './components/pages/MeasurementsList';

const App: React.FC = () => {
    return (
        <Layout>
            <Routes>
                <Route path="/" element={<Home/>}/>
                <Route path="/currentdata" element={<CurrentData/>}/>
                <Route path="/historicaldata" element={<HistoricalData/>}/>
                <Route path="/measurementslist" element={<MeasurementsList/>}/>
            </Routes>
        </Layout>
    );
};

export default App;
