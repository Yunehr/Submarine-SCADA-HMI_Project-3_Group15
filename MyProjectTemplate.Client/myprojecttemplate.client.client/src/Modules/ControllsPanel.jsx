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

    //useEffect(() => {
    //    let mounted = true;
    //    // The Vite dev server proxies /lifesupportpanel to the API (see vite.config.js).       //TODO: Create API link in vite.config.js and MyProjectTemplate.API
    //    fetch('/weatherforecast')                                                               //... : Update to reference /lifesupportpanel
    //        .then((res) => {
    //            if (!res.ok) throw new Error(`HTTP ${res.status}`);
    //            return res.json();
    //        })
    //        .then((json) => mounted && setItems(json))
    //        .catch((err) => mounted && setError(err.message))
    //        .finally(() => mounted && setLoading(false));

    //    // Cleanup pattern prevents setting state after unmount
    //    return () => {
    //        mounted = false;
    //    };
    //}, []);

    //if (loading) return <div>Loading...</div>;
    //if (error) return <div style={{ color: 'red' }}>Error: {error}</div>;
    //if (!items || items.length === 0) return <div>No data</div>;


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
        // fetch('/api/rudder/left', { method: 'POST' })
    };
    const handleRudderRight = () => {
        console.log("Rudder right...");
        // fetch('/api/rudder/right', { method: 'POST' })
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
                        onChange={setPitch}
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
                        onChange={setThrottle}
                        units="Knot"
                        height={150}
                    />
                </div>
                
            </div>


            
        </div>

    );
}