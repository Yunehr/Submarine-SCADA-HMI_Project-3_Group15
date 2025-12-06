import React, { useEffect, useState } from 'react';
import '../App.css';
import SceneButton from '../Components/SceneButton';
import WarningLight from '../Components/WarningLight';

export default function WarningPanel() {
    const [radiation, setRadiation] = useState(null);

    // Thresholds (same as ScramPanel)
    const RADIATION_WARN = 3.0;
    const RADIATION_MAX = 5.0;

    useEffect(() => {
        const fetchRadiation = async () => {
            try {
                const res = await fetch("/api/Reactor/Radiation");
                if (res.ok) {
                    const data = await res.json();
                    setRadiation(data);
                }
            } catch (err) {
                console.error("Radiation fetch failed:", err);
            }
        };

        fetchRadiation();
        const interval = setInterval(fetchRadiation, 500); // poll every 1s
        return () => clearInterval(interval);
    }, []);

    const checkWarning = () => {
        if (!radiation) return false;
        const val = radiation.value;
        return val > 0 && val >= RADIATION_WARN && val < RADIATION_MAX;
    };

    const checkDanger = () => {
        if (!radiation) return false;
        const val = radiation.value;
        return val >= RADIATION_MAX;
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
                    <SceneButton label="Scene 1" />
                    <SceneButton label="Scene 2" />
                    <SceneButton label="Scene 3" />
                    <SceneButton label="Scene 4" />
                </div>
            </div>
        </div>
    );
}
