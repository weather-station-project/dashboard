import React, { useState, useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { ListGroup } from 'react-bootstrap';
import Loading from '../../../Loading';
const axios = require('axios');


function ForecastData() {
    const { t } = useTranslation();
    const [data, setData] = useState(null);

    useEffect(() => {
        async function fetchData() {
            try {
                const result = await axios.get('https://localhost:44375/api/v1/AmbientTemperatures/last');
                setData(result.data);
            } catch (e) {
                setData(() => { throw e; });
            }

        };

        fetchData();
    }, []);

    return (
        <div>
            {data ?
                <ListGroup>
                    <ListGroup.Item>{t('current_data.last_data.ambient_temperature', {
                        temperature: data.temperature,
                        dateTime: new Date(data.dateTime)
                    })}</ListGroup.Item>
                    <ListGroup.Item>Dapibus ac facilisis in</ListGroup.Item>
                    <ListGroup.Item>Morbi leo risus</ListGroup.Item>
                    <ListGroup.Item>Porta ac consectetur ac</ListGroup.Item>
                    <ListGroup.Item>Vestibulum at eros</ListGroup.Item>
                </ListGroup>
                : <Loading />
            }
        </div>);
}

export default ForecastData;