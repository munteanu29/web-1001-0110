import { LocationModel } from "../Models/Location";

export const getUserLocation = (callback: Function) => {
  const success = (position: any) => {
    const location = {
      lat: position.coords.latitude,
      lng: position.coords.longitude,
    };
    callback(location);
  };

  const errorHandler = () => alert("Unable to retrieve your location");

  if (!navigator.geolocation) {
    alert("Geolocation is not supported by your browser");
  } else {
    navigator.geolocation.getCurrentPosition(success, errorHandler);
  }
};
