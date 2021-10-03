import {render, screen} from "@testing-library/react";
import Loading from "../../components/Loading";

describe("Loading", () => {
    it("When_RenderingComponent_Should_RenderExpectedContent", () => {
        const text = "Loading...";

        render(<Loading/>);

        const element = screen.queryByText(text);
        expect(element).toBeInTheDocument();
        expect(element?.tagName.toLowerCase()).toEqual("div");
    })
})