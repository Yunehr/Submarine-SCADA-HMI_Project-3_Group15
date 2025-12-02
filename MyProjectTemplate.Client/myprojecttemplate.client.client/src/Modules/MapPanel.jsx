// Life Support Panel, Displaying various gauges and buttons representing overall health of

import React, { useEffect, useState } from 'react';      //un comment when link to API backend is created

import '../App.css';

export default function Map() {
    const [posx, setposx] = useState(null);
    const [posy, setposy] = useState(null);
    const [posz, setposz] = useState(null);
    // const [speed, setspeed] = useState(null);
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
                    safeFetch('/api/movement/Xpos'),
                    safeFetch('/api/movement/Ypos'),
                    safeFetch('/api/movement/Zpos'),
                ])
                    .then(([posxData, posyData, poszData]) => {
                        if (posxData) setposx(posxData);
                        if (posyData) setposy(posyData);
                        if (poszData) setposz(poszData);
                    })
                    .catch(err => setError(err.message));
            }, 2000); // poll every 2s

            return () => clearInterval(interval);
        }, []);

    if (error) return <div style={{ color: 'red' }}>Error: {error}</div>;


    return (
        <div className="map-console-screen">
            <div className="map-console-label">
                Depth:{posz}   Position: {posx} {posy}  
            </div>
        </div>
        

    );
}