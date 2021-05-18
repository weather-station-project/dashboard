import React, { useState, useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { ListGroup } from 'react-bootstrap';
const axios = require('axios');


function CurrentData() {
    const { t } = useTranslation();
    const [state, setState] = useState([]);

    useEffect(() => {
        async function fetchData() {
            try {
                const resp = await axios.get('https://localhost:44375/api/v1/AmbientTemperatures/last');
                // this.setState({ data: json });
                console.log(resp.data);
            } catch (err) {
                // Error
                if (err.response) {
                    /*
                     * The request was made and the server responded with a
                     * status code that falls out of the range of 2xx
                     */
                    console.log(err.response.data);
                    console.log(err.response.status);
                    console.log(err.response.headers);
                } else if (err.request) {
                    /*
                     * The request was made but no response was received, `error.request`
                     * is an instance of XMLHttpRequest in the browser and an instance
                     * of http.ClientRequest in Node.js
                     */
                    console.log(err.request);
                } else {
                    // Something happened in setting up the request and triggered an Error
                    console.log('Error', err.message);
                }
                console.log(err);
            }
        };

        fetchData();
    }, []);

    return (
        <div style={{ paddingTop: 20 }}>
            <h1>{t('current_data.last_data_title')}</h1>
            <ListGroup>
                <ListGroup.Item>Cras justo odio</ListGroup.Item>
                <ListGroup.Item>Dapibus ac facilisis in</ListGroup.Item>
                <ListGroup.Item>Morbi leo risus</ListGroup.Item>
                <ListGroup.Item>Porta ac consectetur ac</ListGroup.Item>
                <ListGroup.Item>Vestibulum at eros</ListGroup.Item>
            </ListGroup>
        </div>
    );
}

export default CurrentData;