import axios, { AxiosRequestConfig, AxiosResponse } from "axios";

export interface Api {
  readonly getCountry: (data: any) => AxiosResponse<[]>;
}

const baseURL = "https://api-itec.adelin.ninja/api/";

export const Api = () => {

  async function getCountry(country: string) {
    return await axios.get(`${baseURL}Country/GetCountry?countryName=${country}` );
  }

  return {
    getCountry
  };
};

export default Api;
