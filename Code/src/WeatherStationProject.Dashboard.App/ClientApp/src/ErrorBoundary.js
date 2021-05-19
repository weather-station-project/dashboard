import React, { Component } from 'react';
import { Alert } from 'react-bootstrap';

export class ErrorBoundary extends Component {
    constructor(props) {
        super(props);
        this.state = { error: null, errorInfo: null };
    }

    componentDidCatch(error, errorInfo) {
        this.setState({
            error: error,
            errorInfo: errorInfo
        });
    }

    render() {
        if (this.state.errorInfo) {
            return (
            <Alert variant='danger'>
                <Alert.Heading>{this.state.error && this.state.error.toString()}</Alert.Heading>
                <p> {this.state.errorInfo.componentStack}</p>
            </Alert>);
        }
        return this.props.children;
    }
}