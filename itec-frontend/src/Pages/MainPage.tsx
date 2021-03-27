import React, { useEffect, useState } from 'react'
import List from '../Components/List'
import {Map} from '../Components/Map'
import { LocationModel } from '../Models/Location';

export default function MainPage() {
    const [clickedLocation, setClickedLocation]=useState<LocationModel>({
        lat: 45.75346,
        lng: 21.22334,
      });
    
    useEffect(() => {
          console.log(clickedLocation);
      }, [clickedLocation])
      
    return (
        <div className="main">
            <List setLocation={(e:LocationModel)=>setClickedLocation(e)}/>
            <Map location={clickedLocation}/>
        </div>
    )
}
