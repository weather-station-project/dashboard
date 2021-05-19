import React, { useState, useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { ListGroup } from 'react-bootstrap';
import Loading from '../../../Loading';
import ErrorMessage from '../../../ErrorMessage';
const axios = require('axios');


function CurrentData() {
    const { t } = useTranslation();
    const [data, setData] = useState(null);
    const [error, setError] = useState('');

    useEffect(() => {
        async function fetchData() {
            try {
                const result = await axios.get('https://localhost:44375/api/v1/AmbientTemperatures/last');
                setData(result.data);
            } catch (err) {
                setError(err.message);
                setData(null);
            }
        };

        fetchData();
    }, []);

    return (
        <div style={{ paddingTop: 20 }}>
            <h1>{t('current_data.last_data_title')}</h1>
            {error ?
                <ErrorMessage message={error} />
                : data
                    ? <ListGroup>
                        <ListGroup.Item>{t('current_data.last_data.ambient_temperature', {
                            temperature: data.temperature,
                            dateTime: new Date(data.dateTime)
                        })}</ListGroup.Item>
                        <ListGroup.Item>Dapibus} ac facilisis in</ListGroup.Item>
                        <ListGroup.Item>Morbi leo risus</ListGroup.Item>
                        <ListGroup.Item>Porta ac consectetur ac</ListGroup.Item>
                        <ListGroup.Item>Vestibulum at eros</ListGroup.Item>
                    </ListGroup>
                    : <Loading />
            }
        </div>
    );
}

export default CurrentData;