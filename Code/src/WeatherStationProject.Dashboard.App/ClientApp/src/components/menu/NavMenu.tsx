import React from "react";
import { Nav, Navbar } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import LanguageSelector from "./LanguageSelector";
import logo from "./logo.png";

const NavMenu: React.FC = () => {
  const { t } = useTranslation();

  return (
    <header>
      <Navbar bg="primary" variant="dark">
        <Navbar.Brand href="/">
          <img src={logo} className="d-inline-block align-top" alt="Logo" width="50px" height="50px" />
        </Navbar.Brand>
        <Nav className="mr-auto">
          <Nav.Link href="/currentdata">{t("navmenu.current_data")}</Nav.Link>
          <Nav.Link href="/historicaldata">{t("navmenu.historical_data")}</Nav.Link>
          <Nav.Link href="/measurementslist">{t("navmenu.measurements_list")}</Nav.Link>
        </Nav>
        <Nav className="mr-sm-2">
          <LanguageSelector />
        </Nav>
      </Navbar>
    </header>
  );
};

export default NavMenu;
