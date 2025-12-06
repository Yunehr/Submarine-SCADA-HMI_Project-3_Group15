// Life Support Panel, Displaying various gauges and buttons representing overall health of

import React, { useEffect, useState } from 'react';      //un comment when link to API backend is created

import '../App.css';

export default function Map() {
    const [posx, setposx] = useState(0);
    const [posy, setposy] = useState(0);
    const [posz, setposz] = useState(0);
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
            }, 500); // poll every 2s

            return () => clearInterval(interval);
        }, []);

    if (error) return <div style={{ color: 'red' }}>Error: {error}</div>;

    // calculate/translatr position to pixal values on map
    const mapWidth = 22 * 16;  // 22 rem * 16 px 
    const mapHeight = 12 * 16; // 12 rem * 16 px
    const dotSize = 10;        // matches CSS width/height

    // Avoid divide-by-zero by checking range
    const range = 1000; // since posx/posy go from -1000 to +1000
    const safeRange = range === 0 ? 1 : range;

    // Scale logical coordinates (-500..500) to pixel space
    let pixelX = ((posy + 500) / safeRange) * mapWidth;
    let pixelY = (((-posx) + 500) / safeRange) * mapHeight;

    // Center the dot (subtract half its size)
    pixelX -= dotSize / 2;
    pixelY -= dotSize / 2;

    // Clamp to map boundaries
    const clampedX = Math.min(Math.max(pixelX, 0), mapWidth - dotSize);
    const clampedY = Math.min(Math.max(pixelY, 0), mapHeight - dotSize);

    return (
        <div className="map-console-screen">
            <div className="map-console-warning-bar">
                 test
            </div>
            <div className="map-console-map">
                <div
                    className="map-submarine"
                    style={{
                        left: `${clampedX}px`,
                        top: `${clampedY}px`
                    }}
                />
            </div>
            <div className="map-console-depth-label">
                Depth: {posz ? Number(posz).toFixed(0): 0}
            </div>
            <div className="map-console-label">
                Position: {posy ? Number(posy).toFixed(3) : 0 }, {posx ? Number(posx).toFixed(3): 0}
            </div>
        </div>
    );
}