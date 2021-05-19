import React, { Suspense } from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import App from './App';
import Loading from './Loading';
import registerServiceWorker from './registerServiceWorker';
import './i18n';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');

ReactDOM.render(
    <BrowserRouter basename={baseUrl}>
        <Suspense fallback={(<Loading />)}>
            <App useSuspense={true} />
        </Suspense>
    </BrowserRouter >,
    rootElement);

registerServiceWorker();
