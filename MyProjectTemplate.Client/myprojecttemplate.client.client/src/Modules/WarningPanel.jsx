// Life Support Panel, Displaying various gauges and buttons representing overall health of

//import React, { useEffect, useState } from 'react';      //un comment when link to API backend is created

import '../App.css';
import SceneButton from '../Components/SceneButton';
import WarningLight from '../Components/WarningLight'

export default function WarningPanel() {
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

    const checkWarning = () => {
        // check if there is a warning level notice within Sub Systems.
        // fetch('', { method: 'POST' }) etc.
        return false;
    };
    const checkDanger = () => {
        // check if there is a Danger level notice within Sub Systems.
        // fetch('', { method: 'POST' }) etc.
        return false;
    };

    return (
        <div>
            <div className="gauge-row">
                <div className="center">
                    <WarningLight 
                        warning={checkWarning()}
                        danger={checkDanger()}
                    />
                </div>
            </div>
            <div className="center">
                <div className="gauge-row">
                    <SceneButton label="Scene 1"/>
                    <SceneButton label="Scene 2" />
                    <SceneButton label="Scene 3" />
                    <SceneButton label="Scene 4" />
                </div>
            </div>
        </div>

    );
}