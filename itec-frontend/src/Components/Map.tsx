import * as React from 'react';

declare const window: any;
const H = window.H;

var dataPoints: any[] = [];
dataPoints.push(new H.clustering.DataPoint(57.01, 1.01));
dataPoints.push(new H.clustering.DataPoint(33.01, 12.01));
dataPoints.push(new H.clustering.DataPoint(40.01, 3.01));

export const Map = () => {
  // Create a reference to the HTML element we want to put the map on
  const mapRef = React.useRef(null);

  /**
   * Create the map instance
   * While `useEffect` could also be used here, `useLayoutEffect` will render
   * the map sooner
   */
  React.useLayoutEffect(() => {
    // `mapRef.current` will be `undefined` when this hook first runs; edge case that
    if (!mapRef.current) return;
    const platform = new H.service.Platform({
        apikey: "0m3SljGdGlOsmb-s1OajUHK9-IhMaiML60DIZfxiXhw"
    });
    const defaultLayers = platform.createDefaultLayers();
    const hMap = new H.Map(mapRef.current, defaultLayers.vector.normal.map, {
      center: { lat: 50, lng: 5 },
      zoom: 5,
      pixelRatio: window.devicePixelRatio || 1
    });

    var clusteredDataProvider = new H.clustering.Provider(dataPoints);

    // Create a layer that includes the data provider and its data points: 
    var layer = new H.map.layer.ObjectLayer(clusteredDataProvider);

    var behavior = new H.mapevents.Behavior(new H.mapevents.MapEvents(hMap));

    // var noiseMarker = new H.map.Marker(57.01, 1.01);
    // noiseMarker.setData(57.01, 1.01);

    const ui = H.ui.UI.createDefault(hMap, defaultLayers);

    hMap.addLayer(layer);
    //hMap.addLayer(noiseMarker);

    //hMap.addObject(new H.map.Marker.setData(57.01, 1.01));

    // This will act as a cleanup to run once this hook runs again.
    // This includes when the component un-mounts
    return () => {
      hMap.dispose();
    };
  }, [mapRef]); // This will run this hook every time this ref is updated

  return (
      <div className="map">
        <div ref={mapRef} style={{ height: "100vh", width:"100vw"}} />;
      </div>
  )
};
