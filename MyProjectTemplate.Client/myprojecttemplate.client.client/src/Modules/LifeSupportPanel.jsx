// Life Support Panel, Displaying various gauges and buttons representing overall health of

//import React, { useEffect, useState } from 'react';      //un comment when link to API backend is created
import CustomGauge from '../Components/CustomGauge';
//import ToggleButton from './Components/Button-Toggle'
import VerticalSwitch from '../Components/VerticleSwitch'
import '../App.css';

export default function LifeSupportPanel() {
    //const [items, setItems] = useState(null);          //un comment when link to API backend is created
    //const [loading, setLoading] = useState(true);
    //const [error, setError] = useState(null);

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
            Life Support
            <div className="gauge-row">
                <div className="gauge-item"> {/*O2/CO2 Gauges*/}
                    <CustomGauge
                        label="O₂"
                        value={24}
                        min={16} max={25}
                        dLow={18} wLow={19}
                        wHigh={22} dHigh={23}
                        labelType="%"
                    />
                    <CustomGauge
                        label="CO₂"
                        value={450}
                        min={300} max={2500}
                        wLow={400} dLow={301}
                        wHigh={1000} dHigh={2000}
                        labelType="ppm"
                        showLow={false}
                    />
                </div>
            </div>

            <div className="gauge-row switch-row">
                <VerticalSwitch label="O2 Valve" />
                <VerticalSwitch label="Scrubber" />
            </div>
            Pressure Gauges
            <div className="gauge-row">
                
                <div className="gauge-item">
                    <CustomGauge label="temp1" />
                    <CustomGauge label="temp2" />
                </div>
                
            </div>
            <div className="gauge-row">
                Climate Control Panel
            </div>
        </div>
        
    );
}