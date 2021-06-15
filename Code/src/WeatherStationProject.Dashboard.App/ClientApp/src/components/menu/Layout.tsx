import React, { Component, ReactNode } from "react";
import { Container } from "reactstrap";
import NavMenu from "./NavMenu";
import { ErrorBoundary } from "../../ErrorBoundary"

interface IProps {
    children: ReactNode;
}

const layout = (props: IProps): React.FC<IProps> => {
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

export default layout;