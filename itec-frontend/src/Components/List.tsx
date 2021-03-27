import { Button, createStyles, Grid, makeStyles, TextField, Theme } from '@material-ui/core'
import React, { useState } from 'react'
import SearchIcon from '@material-ui/icons/Search';
import DateFnsUtils from '@date-io/date-fns';
import {
  MuiPickersUtilsProvider,
  KeyboardTimePicker,
  KeyboardDatePicker,
} from '@material-ui/pickers';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    container: {
      display: 'flex',
      flexWrap: 'wrap',
    },
    textField: {
      marginLeft: theme.spacing(1),
      marginRight: theme.spacing(1),
      width: 200,
    },
  }),
);

export default function List() {
    const classes = useStyles();
    const [selectedDate, setSelectedDate] = React.useState<Date | null>(
        new Date('2014-08-18T21:11:54'),
      );

    const handleDateChange = (date: Date | null) => {
        setSelectedDate(date);
      };

    return (
        <div className="list">
            <form className={classes.container} noValidate>
                <div className="fields">
                    <TextField 
                        label="Country"
                        id="outlined-size-small"
                        defaultValue=""
                        variant="outlined"
                        size="small"
                    />
                    <TextField
                        id="date"
                        label="Arrival"
                        type="date"
                        defaultValue="2017-05-24"
                        className={classes.textField}
                        InputLabelProps={{
                        shrink: true,
                        }}
                    />
                    <TextField
                        id="date"
                        label="Departure"
                        type="date"
                        defaultValue="2017-05-24"
                        className={classes.textField}
                        InputLabelProps={{
                        shrink: true,
                        }}
                    />
                    <Button><SearchIcon/>Search</Button>
                </div>
            </form>
        </div>
    )
}
