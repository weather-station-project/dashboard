import { render, screen } from "@testing-library/react";
import React from "react";
import { Router } from "react-router-dom";
import App from "../App";
import { createMemoryHistory } from "history";

jest.mock("../components/menu/Layout", () => ({ children }: never) => <>{children}</>);
jest.mock("../components/pages/Home", () => () => <span data-testid="home-id" />);
jest.mock("../components/pages/CurrentData/CurrentData", () => () => <></>);
jest.mock("../components/pages/HistoricalData", () => () => <></>);
jest.mock("../components/pages/MeasurementsList", () => () => <></>);

describe("App", () => {
  beforeEach(() => {
    const history = createMemoryHistory();
    render(
      <Router history={history}>
        <App />
      </Router>
    );
  });

  it("When_RenderingComponent_Should_RenderExpectedContent", () => {
    const element = screen.queryByTestId("home-id");
    expect(element).toBeInTheDocument();
    expect(element?.tagName.toLowerCase()).toEqual("span");
  });
});
