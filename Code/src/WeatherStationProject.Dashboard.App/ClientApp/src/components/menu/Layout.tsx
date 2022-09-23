import React, { ReactNode } from 'react';
import NavMenu from './NavMenu';
import { ErrorBoundary } from '../../ErrorBoundary';
import { Container } from 'react-bootstrap';

interface IProps {
  children: ReactNode;
}

const Layout: React.FC<IProps> = (props) => {
  return (
    <>
      <ErrorBoundary>
        <NavMenu />
      </ErrorBoundary>
      <Container>{props.children}</Container>
    </>
  );
};

export default Layout;
