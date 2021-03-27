import { makeStyles } from "@material-ui/core/styles";
import React, { useState } from "react";
import { Button, Checkbox, Modal, Paper } from "@material-ui/core";
import Api from "../Services/api";

const useStyles = makeStyles(() => ({
  paper: {
    position: "absolute",
    right: "10px",
    height: "200px",
    width: "200px",
    alignItems: "center",
  },
  alignCenter: {
    alignItems: "center",
  },
}));
interface IWeatherResponse {
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

function WeatherModal(props: {
  closeFilterModal: () => void;
  open: any;
  lat: number;
  long: number;
}) {
  const { closeFilterModal, open, lat, long } = props;
  const classes = useStyles();
  const { getWeather } = Api();
  const getWeatherStuff = async () => {
    const result = await getWeather(lat.toString(), long.toString());
    console.log(result.data.weather[0].icon);
    setResponse(result.data);
  };
  const [response, setResponse] = useState<IWeatherResponse>();

  React.useEffect(() => {
    getWeatherStuff();
  }, [open]);

  return (
    <Modal
      open={open}
      onBackdropClick={closeFilterModal}
      aria-labelledby="simple-modal-title"
      aria-describedby="simple-modal-description"
      style={{
        zIndex: 10000,
      }}
    >
      <Paper className={classes.paper}>
        <img
          alt=""
          src={response?.weather[0].icon}
          className={classes.alignCenter}
        />
        <div className={classes.alignCenter}>
          {response?.weather[0].description}
        </div>
        {response?.main.temp}°C
      </Paper>
    </Modal>
  );
}

export default WeatherModal;
