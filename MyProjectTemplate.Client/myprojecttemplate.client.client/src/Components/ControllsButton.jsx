import React from 'react';
import '../App.css';

export default function ButtonControls({
    rLabel = "Fill",
    lLabel = "Empty",
    label = "",
    handleLeft,
    handleRight
}) {
    return (
        <div>
            <div className="controls-row">
                <button
                    className="control-button"
                    onMouseDown={handleLeft}
                    onTouchStart={handleLeft}
                >
                    {lLabel}
                </button>

                <button
                    className="control-button"
                    onMouseDown={handleRight}
                    onTouchStart={handleRight}
                >
                    {rLabel}
                </button>
            </div>
            <div className="control-button-label">{label}</div>
        </div>
    );
}

