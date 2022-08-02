import React from 'react';
import {Spinner} from 'react-bootstrap';

const Loading: React.FC = () => {
  return <Spinner data-testid="loading-spinner" animation="border" role="status"/>;
};

export default Loading;
