import { render, screen } from "@testing-library/react";
import { Router } from "react-router-dom";
import { IAccuWeatherDailyForecast } from "../../../model/OpenWeatherApiTypes";
import { createMemoryHistory } from "history";
import CarouselDailyData from "../../../components/carousel/CarouselDailyData";
import React from "react";

jest.mock("react-i18next", () => ({
  useTranslation: () => {
    return {
      t: (str: string) => str,
    };
  },
}));

describe("CarouselDailyData", () => {
  const response: IAccuWeatherDailyForecast = {
    EpochDate: 0,
    Temperature: {
      Minimum: {
        Value: 10,
      },
      Maximum: {
        Value: 20,
      },
    },
    Day: {
      Icon: 1,
      LongPhrase: "This is a test",
      Wind: {
        Speed: {
          Value: 78,
        },
        Direction: {
          Localized: "TEST",
        },
      },
      WindGust: {
        Speed: {
          Value: 99,
        },
        Direction: {
          Localized: "N",
        },
      },
      Rain: {
        Value: 123,
      },
    },
    Night: {
      Icon: 10,
      LongPhrase: "Tonight",
      Wind: {
        Speed: {
          Value: 780,
        },
        Direction: {
          Localized: "TEST-X",
        },
      },
      WindGust: {
        Speed: {
          Value: 100,
        },
        Direction: {
          Localized: "NO",
        },
      },
      Rain: {
        Value: 1234,
      },
    },
    Link: "testing",
  };

  beforeEach(() => {
    const history = createMemoryHistory();
    render(
      <Router history={history}>
        <CarouselDailyData data={response} />
      </Router>
    );
  });

  it("When_RenderingComponent_Should_RenderExpectedContent", () => {
    const dayPhrase = screen.getByText(response.Day.LongPhrase);
    const link = screen.getByText("Link");

    expect(dayPhrase).toBeInTheDocument();
    expect(dayPhrase?.tagName.toLowerCase()).toEqual("div");

    expect(link).toBeInTheDocument();
    expect(link?.tagName.toLowerCase()).toEqual("a");
  });

  it("When_SwitchingToNight_Should_RenderNightComponents", () => {
    const button = screen.getByTestId("button-night");
    button.click();

    const nightButton = screen.queryByTestId("button-night");
    const dayButton = screen.getByTestId("button-day");

    expect(nightButton).toBeNull();

    expect(dayButton).toBeInTheDocument();
    expect(dayButton?.tagName.toLowerCase()).toEqual("button");
  });
});
