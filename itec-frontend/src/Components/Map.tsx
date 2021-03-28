import * as React from "react";
import { LocationModel } from "../Models/Location";
import { getUserLocation } from "../Services/Location";

declare const window: any;
const H = window.H;

var dataPoints: any[] = [];
dataPoints.push(new H.clustering.DataPoint(57.01, 1.01));
dataPoints.push(new H.clustering.DataPoint(33.01, 12.01));
dataPoints.push(new H.clustering.DataPoint(40.01, 3.01));

export const Map = (props: {
  location: LocationModel;
  nearbyLocations: LocationModel[];
}) => {
  const mapRef = React.useRef(null);

  React.useEffect(() => {
    if (!mapRef.current) return;
    const platform = new H.service.Platform({
      apikey: "waD3Mkr9fcr7L0_y_QF0-jaesYDGNzR3X_u4HQEVM9s",
    });
    const defaultLayers = platform.createDefaultLayers();
    const hMap = new H.Map(mapRef.current, defaultLayers.vector.normal.map, {
      center: props.location,
      zoom: 14,
      pixelRatio: window.devicePixelRatio || 1,
    });

    var clusteredDataProvider = new H.clustering.Provider(dataPoints);
    var layer = new H.map.layer.ObjectLayer(clusteredDataProvider);
    hMap.addLayer(layer);
    var behavior = new H.mapevents.Behavior(new H.mapevents.MapEvents(hMap));

    // add pins on map
    hMap.addObject(new H.map.Marker(props.location));
    props.nearbyLocations.forEach((l) => hMap.addObject(new H.map.Marker(l)));

    return () => {
      hMap.dispose();
    };
  }, [mapRef, props.location, props.nearbyLocations]);

  return (
    <div className="map">
      <div ref={mapRef} style={{ height: "100vh", width: "100vw" }} />;
    </div>
  );
};
