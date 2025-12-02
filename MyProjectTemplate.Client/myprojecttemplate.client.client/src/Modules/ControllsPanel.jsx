// Life Support Panel, Displaying various gauges and buttons representing overall health of


import React, { /*useEffect,*/ useState } from 'react';      //un comment when link to API backend is created
import '../App.css';
import VerticalSlider from '../Components/VerticalSlider'; 
import ButtonControls from '../Components/ControllsButton';

export default function Controls() {
    const [throttle, setThrottle] = useState(0);
    const [pitch, setPitch] = useState(0);


    const sendPitch = (val) => {        // PITCH API call
        fetch('/api/movement/Pitch', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ value: val })
        });
    };

    const sendThrottle = (val) => {     // THROTTLE API call
        fetch('/api/movement/Throttle', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ value: val })
        });
    };

    const handlePitch = (val) => {    // PITCH on button click
        setPitch(val);
        sendPitch(val);
    };

    const handleThrottle = (val) => { // THROTTLE on button click
        setThrottle(val);
        sendThrottle(val);
    };

    const handleRudderLeft = () => {
        fetch('/api/movement/Rudder', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ value: -10 }) // example left turn
        });
    };

    const handleRudderRight = () => {
        fetch('/api/movement/Rudder', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ value: 10 }) // example right turn
        });
    };

    const handleBallastFill = () => {
        fetch('/api/movement/Ballast/Fill', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ value: -10 }) // decrease balast by 10
        });
    };

    const handleBallastEmpty = () => {
        fetch('/api/movement/Ballast/Empty', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ value: 10 }) // increase balast by 10
        });
    };


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