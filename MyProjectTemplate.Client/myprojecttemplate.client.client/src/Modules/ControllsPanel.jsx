// Life Support Panel, Displaying various gauges and buttons representing overall health of


import React, { /*useEffect,*/ useState } from 'react';      //un comment when link to API backend is created
import '../App.css';
import VerticalSlider from '../Components/VerticalSlider'; 
import ButtonControls from '../Components/ControllsButton';

export default function Controls() {
    const [throttle, setThrottle] = useState(0);
    const [pitch, setPitch] = useState(0);
    const [rudder, setRudder] = useState(0);
    const [ballast, setBallast] = useState(0);

    const sendPitch = (val) => {
        fetch('/api/movement/Pitch', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ value: val })
        });
    };

    const sendThrottle = (val) => {
        fetch('/api/movement/Throttle', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ value: val })
        });
    };

    const sendRudder = (val) => {
        fetch('/api/movement/Rudder', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ value: val })
        });
    };

    const sendBallast = (val) => {
        fetch('/api/movement/Ballast', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ value: val })
        });
    };

    // Handlers
    const handlePitch = (val) => {
        setPitch(val);
        sendPitch(val);
    };

    const handleThrottle = (val) => {
        setThrottle(val);
        sendThrottle(val);
    };

    const handleRudderLeft = () => {
        let newVal = rudder - 15;   // decrease by 15 each click  // +- 360 (if greatre than 360 then rollover to set to 0)
        if (newVal <= -360) {
            newVal = 0; // rollover to 0 if rotation reaches -360 or lower
        }
        setRudder(newVal);
        sendRudder(newVal);
    };

    const handleRudderRight = () => {
        let newVal = rudder + 15;   // increase by 15 each click  // +- 360 (if greatre than 360 then rollover to set to 0)
        if (newVal >= 360) {
            newVal = 0; // rollover to 0 if rotation reaches 360 or higher
        }
        setRudder(newVal);
        sendRudder(newVal);
    };

    const handleBallastEmpty = () => {
        const newVal = Math.min(ballast + 20, 100); // clamp to max 100
        setBallast(newVal);
        sendBallast(newVal);
    };

    const handleBallastFill = () => {
        const newVal = Math.max(ballast - 20, -100); // clamp to min -100
        setBallast(newVal);
        sendBallast(newVal);
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