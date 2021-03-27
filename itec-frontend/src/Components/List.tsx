import { Button, TextField } from '@material-ui/core'
import React from 'react'
import SearchIcon from '@material-ui/icons/Search';

export default function List() {
    return (
        <div className="list">
            <form>
                <div className="fields">
                    <TextField 
                        label="Country"
                        id="outlined-size-small"
                        defaultValue=""
                        variant="outlined"
                        size="small"
                    />
                    <TextField 
                        label="Arrival"
                        id="outlined-size-small"
                        defaultValue=""
                        variant="outlined"
                        size="small"
                    />
                    <TextField 
                        label="Departure"
                        id="outlined-size-small"
                        defaultValue=""
                        variant="outlined"
                        size="small"
                    />
                    <Button><SearchIcon/>Search</Button>
                </div>
            </form>
        </div>
    )
}
