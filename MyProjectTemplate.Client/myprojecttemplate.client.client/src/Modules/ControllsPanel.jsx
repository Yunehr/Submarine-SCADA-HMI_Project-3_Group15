// Life Support Panel, Displaying various gauges and buttons representing overall health of


import React, { /*useEffect,*/ useState } from 'react';      //un comment when link to API backend is created
import '../App.css';
import VerticalSlider from '../Components/VerticalSlider'; 
import ButtonControls from '../Components/ControllsButton';

export default function Controls() {
    //const [items, setItems] = useState(null);          //un comment when link to API backend is created
    //const [loading, setLoading] = useState(true);
    //const [error, setError] = useState(null);
    const [throttle, setThrottle] = useState(0);
    const [pitch, setPitch] = useState(0);


    // Button handlers — later replace with API calls
    const handleBallastFill = () => {
        console.log("Ballast filling..."); // any console log does nothnig in the front end, thus is redundant. Only used as placeholder for API calls/implementations
        // fetch('/api/ballast/fill', { method: 'POST' }) etc.
    };
    const handleBallastEmpty = () => {
        console.log("Ballast emptying...");
        // fetch('/api/ballast/empty', { method: 'POST' })
    };

    
    const handleRudderLeft = () => {
        console.log("Rudder left...");
     //   rudder += 10;
        console.log("Rudder right...");
       // fetch('/api/movement/Rudder', {
         //   method: 'POST',
           // headers: { 'Content-Type': 'application/json' },
      //      body: JSON.stringify({ rudder })
       // })
    };

    const handleRudderRight = () => {
      //  rudder -= 10;
        console.log("Rudder right...");
      //  fetch('/api/movement/Rudder', {
      //      method: 'POST',
        //    headers: { 'Content-Type': 'application/json' },
          //  body: JSON.stringify({ rudder })
       // })
    };
    

    const sendPitch = () => {
        fetch('/api/movement/Pitch', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({pitch})
        })
        //    .then(res => res.json())
    }

    const sendThrottle = () => {
        fetch('/api/movement/Throttle', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(-20)
        })
        //    .then(res => res.json())
    }

    const handlePitch = () => {
        setPitch;
        sendPitch;
    }

    const handleThrottle = () => {
        setThrottle;
        sendThrottle;
    }


    return (
        <div>
            Controlls
            <div className="gauge-row">
                <div className="left">
                    <VerticalSlider
                        label="Stern Plate"
                        min={-90}
                        max={90}
                        value={pitch}
                        onChange={handlePitch}
                        units="°"
                        height={150}
                        legendmax="PITCH UP"
                        legendmin="PITCH DOWN"
                    />
                </div>
                <div className="center">
                    <div className="upper">
                        <ButtonControls
                            label="Ballast (Up/Down)"
                            handleLeft={handleBallastEmpty}
                            handleRight={handleBallastFill}
                        />
                    </div>
                    
                    <div className="lower">
                        <ButtonControls
                            label="Rudder"
                            rLabel="Right"
                            lLabel="Left"
                            handleLeft={handleRudderLeft}
                            handleRight={handleRudderRight}
                        />
                    </div>
                    
                </div>
                <div className="right">
                    <VerticalSlider
                        label="Propellor"
                        min={-100}
                        max={100}
                        value={throttle}
                        onChange={handleThrottle}
                        units="Knot"
                        height={150}
                    />
                </div>
                
            </div>


            
        </div>

    );
}