// Life Support Panel, Displaying various gauges and buttons representing overall health of

import React, { useEffect, useState } from 'react'; 
import CustomGauge from '../Components/CustomGauge';
import ClimateMonitor from '../Components/ClimateMonitor';
import VerticalSwitch from '../Components/VerticleSwitch'
import '../App.css';


export default function LifeSupportPanel() {
    const [o2, setO2] = useState(null);
    const [co2, setCo2] = useState(null);
    const [air, setAir] = useState(null);
    const [Pressure, setPressure] = useState(null);
    const [climate, setClimate] = useState({ temp: null, humidity: null });
    const [error, setError] = useState(null);

    useEffect(() => {
        const safeFetch = async (url) => {
            try {
                const res = await fetch(url);
                if (!res.ok) {
                    // Return null if backend says NotFound or error
                    return null;
                }
                // If body is empty, return null instead of crashing
                const text = await res.text();
                return text ? JSON.parse(text) : null;
            } catch (err) {
                console.error(`Fetch failed for ${url}:`, err);
                return null;
            }
        };

        const interval = setInterval(() => {
            Promise.all([
                safeFetch('/api/lifesupport/Oxygen'),
                safeFetch('/api/lifesupport/CO2'),
                safeFetch('/api/lifesupport/AirReserve'),
                safeFetch('/api/lifesupport/Pressure'),
                safeFetch('/api/lifesupport/Temperature'),
                safeFetch('/api/lifesupport/Humidity')
            ])
                .then(([o2Data, co2Data, airData, pressureData, tempData, humidityData]) => {
                    if (o2Data) setO2(o2Data);
                    if (co2Data) setCo2(co2Data);
                    if (airData) setAir(airData);
                    if (pressureData) setPressure(pressureData);
                    if (tempData && humidityData) {
                        setClimate({ temp: tempData.value, humidity: humidityData.value });
                    }
                })
                .catch(err => setError(err.message));
        }, 2000); // poll every 2s

        return () => clearInterval(interval);
    }, []);


    if (error) return <div style={{ color: 'red' }}>Error: {error}</div>;

    return (
        <div>
            Life Support
            <div className="gauge-row">
                <div className="gauge-item"> {/*O2/CO2 Gauges*/}
                    <CustomGauge
                        warning={o2 && (o2.value < 19 || o2.value > 22)}
                        label="O₂"
                        value={o2 ? o2.value : 0}
                        min={16} max={25}
                        dLow={18} wLow={19}
                        wHigh={22} dHigh={23}
                        labelType="%"
                         
                    />
                    <CustomGauge
                        warning={ co2 && (co2.value < 400 || co2.value > 1000)}
                        label="CO₂"
                        value={co2 ? co2.value : 0}
                        min={300} max={2500}
                        wLow={400} dLow={301}
                        wHigh={1000} dHigh={2000}
                        labelType="ppm"
                        showLow={false}
                    />
                </div>
            </div>
            <div className="gauge-row">
                <div className="gauge-row switch-row">
                    <VerticalSwitch label="O2 Valve" />
                    <VerticalSwitch label="Scrubber" />

                </div>
                <div className="gauge-item">
                    <CustomGauge
                        warning={air && (air.value < 50)}
                        label="Air Fill"
                        value={air ? air.value : 0}
                        min={0} max={101}
                        wLow={50} dLow={15}
                        wHigh={100.8} dHigh={100.9}
                        labelType="%"
                        showHigh={false}
                    />
                </div>
            </div>
            <div className="gauge-row">
                <div className="gauge-item">
                    <div className="gauge-item">
                        <CustomGauge
                            warning={Pressure && (Pressure.value > 0.76 || co2.value < 2)}
                            label="Internal."
                            value={Pressure ? Pressure.value : 0}
                            min={0} max={4}
                            wLow={0.76} dLow={0.4}
                            wHigh={2} dHigh={3}
                            labelType="bar"
                        />
                        <CustomGauge
                            warning={Pressure && (Pressure.value < 1.2 || co2.value > 24)}
                            label="External."
                            value={Pressure ? Pressure.value : 0}
                            min={1} max={40}
                            wLow={1.2} dLow={1.1}
                            wHigh={24} dHigh={36}
                            labelType="bar" />
                    </div>
                </div>
            </div>
            <div className="gauge-row">
                <ClimateMonitor
                    warning={climate.temp < 15 || climate.temp > 23 || climate.humidity < 35 || climate.humidity > 55}
                    temperature={climate.temp ?  Number(climate.temp).toFixed(2) : 0}
                    humidity={climate.humidity ? Number(climate.humidity).toFixed(2) : 0}
                    units="°" // UI breaks if I use ° within the component
                />
            </div>
        </div>
        
    );
}