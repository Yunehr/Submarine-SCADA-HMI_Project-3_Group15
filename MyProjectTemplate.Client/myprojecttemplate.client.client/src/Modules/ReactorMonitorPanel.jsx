import React, { useState } from "react";
import "../App.css";

export default function ScramPanel({ warning = false, danger = false }) {
    const [coverOpen, setCoverOpen] = useState(false);

    const toggleCover = () => {
        setCoverOpen(!coverOpen);
    };

    const handleScramClick = () => {
        fetch('/api/lifesupport/scram', { method: 'POST' })
    };

    const lightClass = danger ? "scram-light danger" : warning ? "scram-light warning" : "scram-light off";

    return (
        <div className="scram-panel">
            {/* Corner lights */}
            <div className={`${lightClass} top-left`} />
            <div className={`${lightClass} top-right`} />
            <div className={`${lightClass} bottom-left`} />
            <div className={`${lightClass} bottom-right`} />

            {/* Cover */}
            <div
                className={`scram-cover ${coverOpen ? "open" : "closed"}`}
                onClick={toggleCover}
            ></div>

            {/* SCRAM Button */}
            <button className="scram-button" onClick={handleScramClick}>
                SCRAM
            </button>
        </div>
    );
}
