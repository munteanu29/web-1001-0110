export interface LocationModel {
  lat: number;
  lng: number;
}

export interface Country {
  covidVaccinesRate: number,
  name: string,
  weather: IWeatherResponse
}

export interface IWeatherResponse {
  weather: [
    {
      description: string;
      icon: string;
    }
  ];
  main: {
    temp: number;
  };
}
