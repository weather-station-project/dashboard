import Loading from "../../src/components/Loading";
import {render, screen} from "@testing-library/react";

describe("When_RenderingComponent_Should_RenderExpectedContent", () => {
    const text = "Loading...";
    
    render(<Loading />);
    
    const element = screen.queryByText(text);
    expect(element).toBeInTheDocument();
    expect(element?.tagName.toLowerCase()).toEqual("div");
})