import React, { useEffect, useState } from "react";
import List from "../Components/List";
import { Map } from "../Components/Map";
import WeatherModal from "../Components/WeatherModal";
import { LocationModel } from "../Models/Location";
import { GetNearbyLocations, SearchLocation } from "../Services/HereApi";

export default function MainPage() {
  const [clickedLocation, setClickedLocation] = useState<LocationModel>({
    lat: 45.75346,
    lng: 21.22334,
  });
  const [searchString, setSearchString] = useState<string>("");

  useEffect(() => {}, [clickedLocation]);

  useEffect(() => {
    if (searchString && searchString !== "")
      SearchLocation(searchString).then((res) => {
        setClickedLocation(res.data.items[0].position);
        GetNearbyLocations(res.data.items[0].position).then((res) =>
          console.log(res.data.items.map((i: any) => i.title))
        );
        // res.data.items[0]?.address.city -> tara
      });
  }, [searchString]);

  return (
    <div className="main">
      <List
        location={clickedLocation}
        setLocation={(e: LocationModel) => setClickedLocation(e)}
        searchString={searchString}
        setSearchString={setSearchString}
      />
      <Map location={clickedLocation} />
      {/* <WeatherModal
        lat={clickedLocation.lat}
        long={clickedLocation.lng}
      /> */}
    </div>
  );
}
