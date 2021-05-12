import React, { Component } from 'react';
import { Navbar, Nav, NavDropdown } from 'react-bootstrap';
import logo from './logo.png';
import './NavMenu.css';

export class NavMenu extends Component {
    render() {
        return (
            <header>
                <Navbar bg="primary" variant="dark">
                    <Navbar.Brand href="/">
                        <img
                            src={logo}
                            className="d-inline-block align-top"
                            alt="Logo" />
                    </Navbar.Brand>
                    <Nav className="mr-auto">
                        <Nav.Link href="/">Home</Nav.Link>
                        <Nav.Link href="/counter">Counter</Nav.Link>
                        <Nav.Link href="/fetch-data">Fetch data</Nav.Link>
                    </Nav>
                    <Nav className="mr-sm-2">
                        <NavDropdown title="Dropdown" id="basic-nav-dropdown" className="mr-sm-2">
                            <NavDropdown.Item href="#action/3.1">Action</NavDropdown.Item>
                            <NavDropdown.Item href="#action/3.2">Another action</NavDropdown.Item>
                            <NavDropdown.Item href="#action/3.3">Something</NavDropdown.Item>
                            <NavDropdown.Divider />
                            <NavDropdown.Item href="#action/3.4">Separated link</NavDropdown.Item>
                        </NavDropdown>
                    </Nav>
                </Navbar>
            </header>
        );
    }
}
