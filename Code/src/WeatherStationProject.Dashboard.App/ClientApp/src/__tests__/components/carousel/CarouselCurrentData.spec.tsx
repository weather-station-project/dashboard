import {render, screen} from "@testing-library/react";
import {Router} from "react-router-dom";
import {IAccuWeatherCurrentConditionsResponse} from "../../../model/OpenWeatherApiTypes";
import CarouselCurrentData from "../../../components/carousel/CarouselCurrentData";
import {createMemoryHistory} from "history";

jest.mock('react-i18next', () => ({
    useTranslation: () => {
        return {
            t: (str: string) => str
        };
    },
}));

describe("CarouselCurrentData", () => {
    it("When_RenderingComponent_Should_RenderExpectedContent", () => {
        // arrange
        const response: IAccuWeatherCurrentConditionsResponse = {
            EpochTime: 0,
            WeatherIcon: 38,
            WeatherText: "This is a test",
            Temperature: {
                Metric: {
                    Value: 10
                }
            },
            RelativeHumidity: 20,
            Wind: {
                Direction: {
                    Localized: "ONO"
                },
                Speed: {
                    Metric: {
                        Value: 50,
                    }
                }
            },
            WindGust: {
                Speed: {
                    Metric: {
                        Value: 100
                    }
                }
            },
            UVIndexText: "Low",
            Pressure: {
                Metric: {
                    Value: 1048,
                }
            },
            Precip1hr: {
                Metric: {
                    Value: 10,
                }
            },
            Link: "test",
        }

        // act
        const history = createMemoryHistory();
        render(<Router history={history}><CarouselCurrentData data={response} /></Router>);

        // assert
        const weatherText = screen.getByText(response.WeatherText);
        const link = screen.getByText("Link");

        expect(weatherText).toBeInTheDocument();
        expect(weatherText?.tagName.toLowerCase()).toEqual("div");

        expect(link).toBeInTheDocument();
        expect(link?.tagName.toLowerCase()).toEqual("a");
    })
})