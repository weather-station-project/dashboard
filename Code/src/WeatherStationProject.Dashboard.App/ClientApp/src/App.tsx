import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Layout from './components/menu/Layout';
import Home from './components/pages/Home';
import CurrentData from './components/pages/CurrentData/CurrentData';
import HistoricalData from './components/pages/HistoricalData/HistoricalData';

const App: React.FC = () => {
  return (
    <Layout>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/currentdata" element={<CurrentData />} />
        <Route path="/historicaldata" element={<HistoricalData showChartViewAndGrouping={true} />} />
        <Route path="/measurementslist" element={<HistoricalData showChartViewAndGrouping={false} />} />
      </Routes>
    </Layout>
  );
};

export default App;
