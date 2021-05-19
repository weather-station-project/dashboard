import React from 'react';
import { Alert } from 'react-bootstrap';

function ErrorMessage(props) {
    return (
        <Alert variant='danger'>{props.message}</Alert>
    );
}

export default ErrorMessage;
