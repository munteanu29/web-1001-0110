import axios, { AxiosResponse } from "axios";
import { LocationModel } from "../Models/Location";

export interface Api {
  readonly getCountry: (data: any) => AxiosResponse<[]>;
}

const baseURL = "https://api-itec.adelin.ninja/api/";

export const Api = () => {
  async function getCountry(country: string, location: LocationModel) {
    return await axios.get(
      `${baseURL}Country/GetCountryInfo?countryName=${country}&lat=${location.lat}&lng=${location.lng}`
    );
  }

  return {
    getCountry,
  };
};

export default Api;
