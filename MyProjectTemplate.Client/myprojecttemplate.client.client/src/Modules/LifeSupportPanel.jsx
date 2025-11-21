// Life Support Panel, Displaying various gauges and buttons representing overall health of

//import React, { useEffect, useState } from 'react';      //un comment when link to API backend is created
import CustomGauge from '../Components/CustomGauge';
import ClimateMonitor from '../Components/ClimateMonitor';
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
                        warning={true}
                        label="O₂"
                        value={24}
                        min={16} max={25}
                        dLow={18} wLow={19}
                        wHigh={22} dHigh={23}
                        labelType="%"
                         
                    />
                    <CustomGauge
                        warning={false}
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
                    <CustomGauge
                        warning={true}
                        label="Intern press."
                        min={0} max={4}
                        wLow={0.76} dLow={0.4}
                        wHigh={2} dHigh={3} 
                        labelType="bar"
                    />
                    <CustomGauge
                        warning={false}
                        label="Extern press."
                        min={1} max={40}
                        wLow={1.2} dLow={1.1}
                        wHigh={24} dHigh={36}
                        labelType="bar" />
                </div>
                
            </div>
            <div className="gauge-row">
                <ClimateMonitor
                    warning={true}
                    temperature={20} // replace 20 with getTemperature function
                    humidity={40} // replace 40 with getHumidity function
                    units="°" // UI breaks if I use ° within the component
                />
            </div>
        </div>
        
    );
}