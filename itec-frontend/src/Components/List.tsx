import { Button, createStyles, Grid, makeStyles, TextField, Theme } from '@material-ui/core'
import React, { useState } from 'react'
import SearchIcon from '@material-ui/icons/Search';
import Api from '../Services/api';
import { Country, LocationModel } from '../Models/Location';

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

export default function List(props:{setLocation:any}) {
    const classes = useStyles();
    const [selectedDate, setSelectedDate] = React.useState<Date | null>(new Date(),);
    const [country, setCountry]=useState("");
    const [countryData, setCountryData]=useState<Country>({covidVaccinationRate:0});
    const [locations, setLocations]=useState([]);
    const {getCountry}=Api();

  const handleDateChange = (date: Date | null) => {
    setSelectedDate(date);
  };

    const getCountryConst=async()=>
    {
      var response = await getCountry(country);
      setLocations(response.data.locationEntities);
      setCountryData(response.data);
      console.log(response.data);
    }

    function search()
    {
      getCountryConst();
      console.log(locations);
    }

    function showLocation(x:number, y:number)
    {
      var location:LocationModel = {
        lat:x,
        lng:y
      }
      props.setLocation(location);
    }
    return (
        <div className="list">
            <form className={classes.container} noValidate>
                <div className="fields">
                    <TextField 
                        label="Country"
                        id="outlined-size-small"
                        defaultValue={country}
                        variant="outlined"
                        size="small"
                        onChange={(e)=> setCountry(e.target.value)}
                    />
                    <TextField
                        id="date"
                        label="Arrival"
                        type="date"
                        defaultValue={selectedDate}
                        className={classes.textField}
                        InputLabelProps={{
                        shrink: true,
                        }}
                    />
                    <TextField
                        id="date"
                        label="Departure"
                        type="date"
                        defaultValue={selectedDate}
                        className={classes.textField}
                        InputLabelProps={{
                        shrink: true,
                        }}
                    />
                    <Button onClick={()=>search()}><SearchIcon/>Search</Button>
                </div>
            </form>
            {countryData.covidVaccinationRate!==0? 
              <div className="covidRate">
                <h4>Covid Vaccination Rate: &nbsp;&nbsp;</h4>
                <h3 style={{color:"#228B22"}}>{countryData.covidVaccinationRate}%</h3>
              </div> 
              : ''}
            {locations.length!==0?
              <div className="locations">
                <h4>Locations:</h4>
                {
                  locations.map((l: any)=>
                  {
                      return(
                        <div className="location" onClick={()=>showLocation(l.xCoordonate, l.yCoordonate)}>
                          <h4>{l.name}</h4>
                          <h4>{l.price} &#8364;</h4>
                        </div>
                      )
                  })
                }
              </div>
            :""}
        </div>
  );
}
