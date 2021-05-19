import React, { Component } from 'react';
import { Container } from 'reactstrap';
import NavMenu from './NavMenu';
import { ErrorBoundary } from '../../ErrorBoundary'

export class Layout extends Component {
    render() {
        return (
            <div>
                <ErrorBoundary>
                    <NavMenu />
                </ErrorBoundary>
                <Container>
                    {this.props.children}
                </Container>
            </div>
        );
    }
}
