import React from "react";
import "../App.css";

export default function SceneButton({ label = "Scenario", alert = false }) {
    // Map labels to backend endpoints
    const endpointMap = {
        "Scene 1": "/api/lifesupport/Oxygen Low",
        "Scene 2": "/api/lifesupport/Pressure Loss",
        "Scene 3": "/api/lifesupport/CO2 Spike",
        Scrubber: '/api/lifesupport/scrubber',
        'O2 Valve': '/api/lifesupport/OxygenGeneration',
        'Pressurize': '/api/lifesupport/Pressurize'
        //"Scene 4": "/api/lifesupport/Reactor Critical Scenario"
    };

    const handleClick = () => {
        const endpoint = endpointMap[label];
        if (!endpoint) {
            console.warn(`No endpoint mapped for ${label}`);
            return;
        }

        fetch(endpoint, {
            method: "POST",
            headers: { "Content-Type": "application/json" }
        })
            .then(res => res.json())
            .then(data => console.log(`${label} response:`, data))
            .catch(err => console.error(`${label} command failed:`, err));
    };

    return (
        <div>
            <div className="scene-panel">
                <button
                    className={`scene-button ${alert ? "alert" : ""}`}
                    onClick={handleClick}
                />
            </div>
            <div className="scene-label">{label}</div>
        </div>

    );
}
