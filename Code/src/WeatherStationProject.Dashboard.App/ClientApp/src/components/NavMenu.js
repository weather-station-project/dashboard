import React from 'react';
import { Navbar, Nav } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import LanguageSelector from './LanguageSelector';
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
                    <LanguageSelector />
                </Nav>
            </Navbar>
        </header>
    );
}

export default NavMenu;