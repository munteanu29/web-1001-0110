import React, { useEffect, useState } from "react";
import List from "../Components/List";
import { Map } from "../Components/Map";
import WeatherModal from "../Components/WeatherModal";
import { LocationModel } from "../Models/Location";
import { GetNearbyLocations, SearchLocation } from "../Services/HereApi";
import { getUserLocation } from "../Services/Location";

export default function MainPage() {
  const [selectedLocation, setSelectedLocation] = useState<{
    country: string;
    location: LocationModel;
  }>({
    location: { lat: 45.75346, lng: 21.22334 },
    country: "Romania",
  });
  const [searchString, setSearchString] = useState<string>("");
  const [nearbyLocations, setNearbyLocations] = useState<LocationModel[]>([]);

  React.useEffect(() => {
    // getUserLocation(setSelectedLocation);
  }, []);

  useEffect(() => {
    if (selectedLocation && selectedLocation.location)
      GetNearbyLocations(selectedLocation?.location).then((res) =>
        setNearbyLocations(res.data.items.map((i: any) => i.position))
      );
  }, [selectedLocation]);

  useEffect(() => {
    if (searchString && searchString !== "")
      SearchLocation(searchString).then((res) => {
        setSelectedLocation({
          location: res.data?.items[0]?.position,
          country: res.data.items[0]?.address.countryName,
        });
      });
  }, [searchString]);

  return (
    <div className="main">
      <List
        location={selectedLocation}
        setLocation={(e: LocationModel) =>
          setSelectedLocation({ ...selectedLocation, location: e })
        }
        searchString={searchString}
        setSearchString={setSearchString}
      />
      <Map
        location={selectedLocation.location}
        nearbyLocations={nearbyLocations}
      />
    </div>
  );
}
