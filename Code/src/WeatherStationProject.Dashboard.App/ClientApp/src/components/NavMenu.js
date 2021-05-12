import React from 'react';
import { Navbar, Nav, NavDropdown } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import logo from './logo.png';
import './NavMenu.css';

function NavMenu() {
    const { t } = useTranslation();

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
                    <Nav.Link href="/">{t('navmenu.current_data')}</Nav.Link>
                    <Nav.Link href="/counter">{t('navmenu.historical_data')}</Nav.Link>
                    <Nav.Link href="/fetch-data">{t('navmenu.measurements_list')}</Nav.Link>
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

export default NavMenu;