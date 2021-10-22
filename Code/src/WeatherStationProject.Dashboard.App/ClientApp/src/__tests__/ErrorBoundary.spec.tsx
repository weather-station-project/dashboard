import { render, screen } from "@testing-library/react";
import { ErrorBoundary } from "../ErrorBoundary";

describe("ErrorBoundary", () => {
  it("When_RenderingComponent_Given_No_Errors_Should_RenderExpectedContent", () => {
    render(
      <ErrorBoundary>
        <span data-testid="id" />
      </ErrorBoundary>
    );

    const element = screen.queryByTestId("id");
    expect(element).toBeInTheDocument();
    expect(element?.tagName.toLowerCase()).toEqual("span");
  });
});
