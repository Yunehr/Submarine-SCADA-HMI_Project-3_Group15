// Life Support Panel, Displaying various gauges and buttons representing overall health of


import React, { /*useEffect,*/ useState } from 'react';      //un comment when link to API backend is created
import '../App.css';
import VerticalSlider from '../Components/VerticalSlider';

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
                <div className="center">Center Panel
                    <div>Ballast Up/down</div>
                    <div className="lower">Rudder Left/Right</div>
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