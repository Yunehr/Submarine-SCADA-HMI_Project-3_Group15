import React, { useState, useEffect } from 'react';
import '../App.css';

export default function VerticalSwitch({ label = 'Master', size = 46 }) {
    const [on, setOn] = useState(false);

    // Map labels to backend endpoints
    const endpointMap = {
        Scrubber: '/api/lifesupport/scrubber',
        'O2 Valve': '/api/lifesupport/OxygenGeneration',
        'Pressurize': '/api/lifesupport/Pressurize'
    };

    // When toggled, fire initial POST
    const toggle = () => {
        const newState = !on;
        setOn(newState);

        if (newState) {
            // Turn ON → call backend immediately
            callBackend();
        } else {
            // Turn OFF → stop loop
            console.log(`${label} turned OFF`);
        }
    };

    // Function to call backend
    const callBackend = () => {
        const endpoint = endpointMap[label];
        if (!endpoint) {
            console.warn(`No endpoint mapped for ${label}`);
            return;
        }

        fetch(endpoint, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' }
        })
            .then(res => res.json())
            .then(data => console.log(`${label} response:`, data))
            .catch(err => console.error(`${label} command failed:`, err));
    };

    // Keep running while ON
    useEffect(() => {
        if (!on) return;

        // Call backend every 5 seconds while ON
        const interval = setInterval(() => {
            callBackend();
        }, 5000);

        return () => clearInterval(interval);
    }, [on]);

    return (
        <div className="switch-panel" style={{ ['--switch-size']: `${size}px` }}>
            <div className="switch-label">{label}</div>
            <button
                type="button"
                className={`vertical-switch ${on ? 'on' : 'off'}`}
                onClick={toggle}
                aria-pressed={on}
                title={`${label} ${on ? 'On' : 'Off'}`}
            >
                <div className="switch-track" />
                <div className="switch-knob" />
            </button>
        </div>
    );
}
