// Top-level React component for the client app.
// Keep this component small in templates; split layout, routes, and pages as the app grows.

import React from 'react';
import LifeSupportPanel from './Modules/LifeSupportPanel';
import Controlls from './Modules/ControllsPanel'
import WarningPanel from './Modules/WarningPanel'
import ReactorMonitor from './Modules/ReactorMonitorPanel'
import Map from './Modules/MapPanel'
import './App.css';


// Additional small demo components to show top/bottom split
function WeaponsPanel() {
    return <div className="panel">Weapons Controls</div>;
}

export default function App() {
    return (
        <div className="app-root">
            <header className="app-header">
                <div>$15 Footlong - Submarine SCADA/HMI</div>
            </header>

            {/* Three-column layout */}
            <section className="layout-columns">
                {/* Left-most sidebar with top + bottom */}
                <div className="sidebar">
                    <div className="sidebar-top grow-small">
                        <WarningPanel />
                        {/* place larger items here (maps, charts, large widgets) */}
                    </div>
                    <div className="sidebar-bottom grow-large">
                        <ReactorMonitor />
                        {/* place smaller, content-sized widgets here */}
                    </div>
                </div>

                {/* Center column split into two sections (top / bottom) */}
                <main className="centerbar">
                    <div className="center-top">
                        <Map />
                        {/* Place maps, toolbars, or top controls here */}
                    </div>

                    <div className="center-bottom">
                        <Controlls />
                        {/* Place lower controls, logs, or detail panels here */}
                    </div>
                </main>

                {/* Right column with top + bottom */}
                <aside className="sidebar">
                    <div className="sidebar-top grow-large">
                        <LifeSupportPanel />
                    </div>
                    <div className="sidebar-bottom auto-size">
                        <WeaponsPanel />
                    </div>
                </aside>
            </section>
        </div>
    );
}
