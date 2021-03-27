import {
  Button,
  createStyles,
  makeStyles,
  TextField,
  Theme,
} from "@material-ui/core";
import React, { useEffect, useState } from "react";
import SearchIcon from "@material-ui/icons/Search";
import Api from "../Services/api";
import { Country, LocationModel } from "../Models/Location";
import WeatherModal from "./WeatherModal";

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    container: {
      display: "flex",
      flexWrap: "wrap",
    },
    textField: {
      marginLeft: theme.spacing(1),
      marginRight: theme.spacing(1),
      width: 200,
    },
  })
);

export default function List(props: {
  setLocation: any;
  location: {
    country: string;
    location: LocationModel;
  };
  searchString: string;
  setSearchString: (e: string) => void;
}) {
  const classes = useStyles();
  const [searchString, setSearchString] = useState("");
  const [countryData, setCountryData] = useState<Country>({
    covidVaccinesRate: 0,
    name: "",
    weather: { weather: [{ description: "", icon: "" }], main: { temp: 0 } },
  });
  const { getCountry } = Api();

  useEffect(() => {
    getCountry(props.location?.country, props.location?.location).then(
      (response) =>
        // setLocations(response.data.locationEntities);
        setCountryData(response.data)
    );
  }, []);

  return (
    <div className="list">
      <form className={classes.container} noValidate>
        <div className="fields">
          <TextField
            label="Search a location"
            id="outlined-size-small"
            defaultValue={searchString}
            variant="outlined"
            size="small"
            onChange={(e) => setSearchString(e.target.value)}
          />
          <Button onClick={() => props.setSearchString(searchString)}>
            <SearchIcon />
            Search
          </Button>
        </div>
      </form>
      {countryData.covidVaccinesRate !== 0 ? (
        <div>
          <div className="covidRate">
            <h4>Covid Vaccination Rate: &nbsp;&nbsp;</h4>
            <h4 style={{ color: "#228B22" }}>
              {countryData.covidVaccinesRate}%
            </h4>
          </div>
          <div className="weatherDiv">
            <WeatherModal weather={countryData.weather} />
          </div>
        </div>
      ) : (
        ""
      )}
    </div>
  );
}
