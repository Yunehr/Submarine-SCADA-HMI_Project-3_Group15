// Life Support Panel, Displaying various gauges and buttons representing overall health of

import React, { useEffect } from 'react'; 

import '../App.css';
import WarningLight from '../Components/WarningLight'

export default function WarningPanel() {

    
    useEffect(() => {
        fetch('/api/scada/alarms')
            .then(res => res.json())
            .then(data => {
                switch (data.some(a => a.Severity)) {
                    case "Warning":
                        setWarning(true);
                        break;
                    case "Danger":
                        setDanger(true)
                        break;
                    default:
                        setWarning(false);
                        setDanger(false);
                }
            });
    }, []);

    const setWarning = (level) => {
        return level;
    };
    const setDanger = (level) => {
        return level;
    };

    return (
        <div className="gauge-row">
            <div className="center">
                <WarningLight 
                    warning={setWarning()}
                    danger={setDanger()}
                />
            </div>
        </div>

    );
}