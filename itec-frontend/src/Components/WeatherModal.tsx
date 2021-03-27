import { makeStyles } from "@material-ui/core/styles";
import React, { useState } from "react";
import { Button, Checkbox, Modal, Paper } from "@material-ui/core";
import Api from "../Services/api";
import { IWeatherResponse } from "../Models/Location";


function WeatherModal(props: {
  weather:IWeatherResponse
}) {
  const {weather} = props;

  return (
    <div className="weather">
        <img alt="" src={weather.weather[0].icon}/>
        <div style={{display:"flex", flexDirection:"column"}}>
          <h4>{weather.weather[0].description}</h4>
          <h4>{weather.main.temp}°C</h4>
        </div>
    </div>
  );
}

export default WeatherModal;
