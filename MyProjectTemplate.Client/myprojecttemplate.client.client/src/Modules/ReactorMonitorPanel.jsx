import React, { useState, useEffect } from "react";
import "../App.css";

export default function ScramPanel() {
    const [coverOpen, setCoverOpen] = useState(false);
    const [coolant, setCoolant] = useState(null);
    const [fuelRod, setFuelRod] = useState(null);
    const [radiation, setRadiation] = useState(null);
    //const [reactorTemp, setReactorTemp] = useState(null);

    const toggleCover = () => setCoverOpen(!coverOpen);

    const handleScramClick = () => {
        fetch("/api/LifeSupport/SCRAM Reactor", { method: "POST" })
            .then(res => res.json())
            .then(data => console.log("SCRAM executed:", data))
            .catch(err => console.error("SCRAM failed:", err));
    };

    useEffect(() => {
        const fetchData = async () => {
            try {
                const [coolantRes, fuelRes, radRes/*, tempRes*/] = await Promise.all([
                    fetch("/api/LifeSupport/Coolant"),
                    fetch("/api/LifeSupport/FuelRod"),
                    fetch("/api/LifeSupport/Radiation"),
                    fetch("/api/LifeSupport/ReactorTemp")
                ]);

                if (coolantRes.ok) setCoolant(await coolantRes.json());
                if (fuelRes.ok) setFuelRod(await fuelRes.json());
                if (radRes.ok) setRadiation(await radRes.json());
                //if (tempRes.ok) setReactorTemp(await tempRes.json());
            } catch (err) {
                console.error("Reactor fetch failed:", err);
            }
        };

        fetchData();
        const interval = setInterval(fetchData, 3000);
        return () => clearInterval(interval);
    }, []);

    // Thresholds from backend constants
    const FUEL_ROD_MIN = 50.0;  
    const FUEL_ROD_CRITICAL = 30.0;
    const COOLANT_MIN = 50.0;
    const COOLANT_CRITICAL = 30.0;

    // these ones are placeholders since i dont actually know what we want to have for good ranges
    const RADIATION_WARN = 3.0;
    const RADIATION_MAX = 5.0;
    const TEMP_MIN = 200.0;
    const TEMP_MAX = 600.0;
    const TEMP_WARN_LOW = 250.0;
    const TEMP_WARN_HIGH = 550.0;

    // Utility function: decide which light class to use based on sensor reading + thresholds
    const getLightClass = (reading, thresholds) => {
        // No reading → light off
        if (!reading) return "scram-light off";

        const val = reading.value;

        // Critical low or high → danger (red)
        if (val <= thresholds.critical || val >= thresholds.max) return "scram-light danger";

        // Approaching limits → warning (yellow)
        if (val <= thresholds.min || val >= thresholds.warnHigh || val <= thresholds.warnLow) return "scram-light warning";

        // Otherwise safe → on (green)
        return "scram-light on";
    };

    // --- Implementation for each subsystem ---

    // Coolant system light:
    // - Uses backend constants for minimum and critical thresholds.
    // - max and warnHigh are set slightly above nominal (100.1 / 100.2) to catch overfill conditions.
    const coolantLight = getLightClass(coolant, {
        min: COOLANT_MIN,              // safe minimum coolant level
        critical: COOLANT_CRITICAL,    // critical low coolant level
        max: 100.2,                    // absolute maximum (overfill)
        warnLow: COOLANT_MIN,          // warning if at minimum
        warnHigh: 100.1                // warning if approaching max
    });

    // Fuel rod integrity light
    // - Same pattern as coolant, since both share similar thresholds.
    const fuelRodLight = getLightClass(fuelRod, {
        min: FUEL_ROD_MIN,
        critical: FUEL_ROD_CRITICAL,
        max: 100.2,
        warnLow: FUEL_ROD_MIN,
        warnHigh: 100.1
    });

    // Radiation level light
    // - Critical threshold is 0 (any measurable radiation above safe background is concerning).
    // - Danger if radiation exceeds max.
    // - Warning if radiation exceeds nominal background but not yet critical.
    const radiationLight = getLightClass(radiation, {
        min: 0,
        critical: 0,
        max: RADIATION_MAX,
        warnLow: 0.001,
        warnHigh: RADIATION_WARN
    });

    // Reactor temperature light
    // - WarnLow and WarnHigh provide early warning zones before hitting critical extremes.
    // - Danger if temp drops below min or exceeds max.
    // - Warning if approaching low or high thresholds.
    //const reactorTempLight = getLightClass(reactorTemp, {               // commented out since not implemented
    //    min: TEMP_MIN,
    //    critical: TEMP_MIN,
    //    max: TEMP_MAX,
    //    warnLow: TEMP_WARN_LOW,
    //    warnHigh: TEMP_WARN_HIGH
    //});


    return (
        <div className="scram-panel">
            <div className={`${coolantLight} top-left`} />
            <div className={`${radiationLight} top-right`} />
            <div className={`${fuelRodLight} bottom-left`} />
            {/*<div className={`${reactorTempLight} bottom-right`} />*/}
            <div className={`scram-light off bottom-right`} />

            <div
                className={`scram-cover ${coverOpen ? "open" : "closed"}`}
                onClick={toggleCover}
            ></div>

            <button className="scram-button" onClick={handleScramClick}>
                SCRAM
            </button>
        </div>
    );
}
