import React, { useState } from 'react';
import '../App.css';

export default function VerticalSwitch({ label = 'Master', size = 46 }) {
    const [on, setOn] = useState(false);

    const toggle = () => {
        const newState = !on;
        setOn(newState);

        // Send command to backend
        fetch(`/api/lifesupport/${label.toLowerCase()}`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                deviceType: label,   // use the label to identify device
                action: newState ? 'ON' : 'OFF'
            })
        }).catch(err => console.error('Switch command failed:', err));
    };

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
