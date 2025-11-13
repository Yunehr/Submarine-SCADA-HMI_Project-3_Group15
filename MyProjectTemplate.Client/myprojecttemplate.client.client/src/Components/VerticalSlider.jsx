import React from 'react';
import '../App.css';

export default function VerticalSlider({
    label = 'Throttle',
    legendmax = 'FWD',
    legendmin = 'BACK',
    min = 0,
    max = 100,
    value,
    onChange,
    step = 1,
    units = '%',
    height = 220,
}) {

    return (
        <div className="gauge-wrapper">
            <div className="gauge-label-row">
                <div className="gauge-readout">{value} {units}</div>
            </div>

            <div className="gauge-body">
                <div className="gauge-casing" style={{ height: `${height}px` }}>
                    <div className="gauge-track">
                        {[0, 25, 50, 75, 100].map(t => (
                            <div key={t} className="gauge-tick" style={{ bottom: `${t}%` }}>
                                <span className="gauge-tick-line" />
                                <span className="gauge-tick-label">
                                    {Math.round(min + (t / 100) * (max - min))}
                                </span>
                            </div>
                        ))}
                        <input
                            type="range"
                            className="gauge-input"
                            min={min}
                            max={max}
                            step={step}
                            value={value}
                            onChange={e => onChange(Number(e.target.value))}
                            aria-label={label}
                        />
                    </div>

                    <div className="gauge-legends">
                        <span className="gauge-legend-max">{legendmax}</span>
                        <span className="gauge-legend-min">{legendmin}</span>
                    </div>
                </div>

            </div>
            <label className="vertical-gauge-label">{label}</label>
        </div>
    );
}
