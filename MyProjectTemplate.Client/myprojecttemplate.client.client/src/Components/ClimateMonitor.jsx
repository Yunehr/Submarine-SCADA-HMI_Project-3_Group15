import React from 'react';
import '../App.css';

export default function ClimateMonitor({ temperature = 22, humidity = 45, units = "°" , warning = false}) {
    return (
        <div className="center">
            {warning ? <div class="status-indicator-cc warning"></div> : <div class="status-indicator-cc off"></div>}
            <div className="climate-monitor">
                <div className="climate-header">Climate Control</div>

                <div className="climate-row">
                    <div className="climate-gauge">
                        <div className="climate-label">Temperature</div>
                        <div className="climate-value">{temperature}{units}C</div>
                    </div>

                    <div className="climate-gauge">
                        <div className="climate-label">Humidity</div>
                        <div className="climate-value">{humidity}%</div>
                    </div>
                </div>
            </div>
        </div>
    );
}
