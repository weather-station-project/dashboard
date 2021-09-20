import {AxiosRequestConfig} from "axios";

interface IAuthenticationToken {
    accessToken: string;
    expiresIn: number;
}

export async function getAxiosRequestConfig(apiHost: string, authHost: string, secret: string): Promise<AxiosRequestConfig> {
    const authToken = await getAuthToken(authHost, secret);
    
    return {
        headers: {
            'Authorization': `Bearer ${authToken.accessToken}`,
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        method: "GET",
        baseURL: apiHost
    };
}

async function getAuthToken(host: string, secret: string): Promise<IAuthenticationToken> {
    const url = '/api/v1/authentication/';
    
    return fetch(host + url + secret, {
        method: "GET", headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        }
    }).then((response) => {
        console.debug(`${url} response: ${response}`);
        
        if (response.status !== 200) {
            throw new Error(`getAuthToken Error: Did not get a 200 back... -> ${response.status}: ${response.statusText}`);
        }

        return response.json();
    }).then((data) => data as IAuthenticationToken);
}
