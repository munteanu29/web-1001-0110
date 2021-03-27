import axios, { AxiosRequestConfig, AxiosResponse } from "axios";

export interface Api {
  readonly getCountry: (data: any) => AxiosResponse<[]>;
}

const baseURL = "https://api-itec.adelin.ninja/api/";

export const Api = () => {
  async function getCountry(country: string) {
    return await axios.get(
      `${baseURL}Country/GetCountry?countryName=${country}`
    );
  }
  async function getWeather(lat: string, long: string) {
    return await axios.get(`${baseURL}Weather/GetWeather?x=${lat}&y=${long}`);
  }

  return {
    getCountry,
    getWeather,
  };
};

export default Api;
