import React, {Suspense} from 'react';
import {BrowserRouter} from 'react-router-dom';
import App from './App';
import './i18n';
import Loading from './components/Loading';
import {createRoot} from 'react-dom/client';

const BaseUrl: string = document.getElementsByTagName('base')[0].getAttribute('href') || '';
const root = createRoot(document.getElementById('root')!);

root.render(
    <React.StrictMode>
        <BrowserRouter basename={BaseUrl}>
            <Suspense fallback={<Loading/>}>
                <App/>
            </Suspense>
        </BrowserRouter>
    </React.StrictMode>
);
