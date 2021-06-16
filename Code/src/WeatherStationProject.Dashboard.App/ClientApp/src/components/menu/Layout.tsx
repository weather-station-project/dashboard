import React, { ReactNode } from "react";
import { Container } from "reactstrap";
import NavMenu from "./NavMenu";
import { ErrorBoundary } from "../../ErrorBoundary"

interface IProps {
    children: ReactNode;
}

const Layout: React.FC<IProps> = (props) => {
    return (
        <div>
            <ErrorBoundary>
                <NavMenu />
            </ErrorBoundary>
            <Container>
                {props.children}
            </Container>
        </div>
    );
};

export default Layout;