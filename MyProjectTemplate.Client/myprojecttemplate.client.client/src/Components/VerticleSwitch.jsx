import React, { useState } from 'react';
import '../App.css';

export default function VerticalSwitch({ label = 'Master', size = 46 }) {
    const [on, setOn] = useState(false);
    const toggle = () => setOn(prev => !prev);

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