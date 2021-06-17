import axios, { AxiosInstance } from "axios";
import { IAuthenticationToken } from "../model/AuthenticationToken";

export class WeatherStationDataApi {
    api: AxiosInstance;
    apiHost: string;
    authServiceHost: string;
    secret: string;

    constructor(apiHost: string, authServiceHost: string, secret: string) {
        console.error("apiHost");

        this.apiHost = apiHost;
        this.authServiceHost = authServiceHost;
        this.secret = secret;

        this.api = axios.create({ url: apiHost });
        this.api.interceptors.request.use(
            async config => {
                const token = await this.getAuthToken();
                config.headers = {
                    'Authorization': `Bearer ${token}`,
                    'Accept': "application/json",
                    'Content-Type': "application/json"
                }
                return config;
            },
            error => {
                Promise.reject(error);
        });
    }

    private async getAuthToken(): Promise<string> {
        return axios.get<IAuthenticationToken>(this.authServiceHost + "/" + this.secret)
            .then((response) => {
                if (response.status !== 200) {
                    throw new Error(`getToken Error: Did not get a 200 back... -> ${response.status}: ${response.statusText}`);
                }
                return response.data.accessToken;
        });
    }

    getThis(): string {
        return this.apiHost;
    }
}