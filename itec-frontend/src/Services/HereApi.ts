import axios from "axios";
import { LocationModel } from "../Models/Location";

const apiKey = process.env.REACT_APP_API_KEY;

export const SearchLocation = async (searchString: string) => {
  return axios.get(
    `https://geocode.search.hereapi.com/v1/geocode?q=${searchString}&apiKey=${apiKey}`
  );
};

export const GetNearbyLocations = async (location: LocationModel) => {
  return axios.get(
    `https://browse.search.hereapi.com/v1/browse?at=${location.lat},${location.lng}&limit=20&categories=350&apiKey=${apiKey}`
  );
};
