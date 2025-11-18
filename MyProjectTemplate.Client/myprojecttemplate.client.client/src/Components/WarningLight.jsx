import React from 'react';
import '../App.css';

export default function WarningLight({ warning = false, danger = false }) {
    let stateClass = "off";

    if (danger) {
        stateClass = "danger";
    } else if (warning) {
        stateClass = "warning";
    }

    return (
        <div className="cw-border">
            <div className={`status-indicator-central-warning ${stateClass}`}></div>
        </div>
    );
}
