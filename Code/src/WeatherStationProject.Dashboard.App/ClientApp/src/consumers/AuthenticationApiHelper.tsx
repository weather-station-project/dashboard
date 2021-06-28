import { AxiosRequestConfig } from "axios";

interface IAuthenticationToken {
    accessToken: string;
    expiresIn: number;
}

export async function getAxiosRequestConfig(apiHost:string, authHost: string, secret: string): Promise<AxiosRequestConfig> {
    const authToken = await GetAuthToken(authHost, secret);
    const config: AxiosRequestConfig = {
        headers: {
            'Authorization': `Bearer ${authToken.accessToken}`,
            'Accept': "application/json",
            'Content-Type': "application/json",
        },
        method: "GET",
        baseURL: apiHost
    }

    return config;
}

async function GetAuthToken(host: string, secret: string): Promise<IAuthenticationToken> {
    const url = "/api/v1/authentication/";
    return fetch(host + url + secret, {
        method: "POST", headers: {
            'Content-Type': "application/x-www-form-urlencoded",
        },
        body: `secret=${secret}`, }).then((response) => {
        if (response.status !== 200) {
            throw new Error(`GetAuthToken Error: Did not get a 200 back... -> ${response.status}: ${response.statusText}`);
        }

        return response.json();
    }).then((data) => data as IAuthenticationToken);
}
