// Top-level React component for the client app.
// Keep this component small in templates; split layout, routes, and pages as the app grows.

import React from 'react';
import LifeSupportPanel from './LifeSupportPanel';
import './App.css'; // Example: add app-level styles here.

function WarningPanel() { // temporary for proof of concept, this will be a separate file
    return <div className="panel">Warning Light</div>;
}
function ReactorMonitor() { // temporary for proof of concept, this will be a separate file
    return <div className="panel">Reactor Monitor (components go here)</div>;
}
function Map() { // temporary for proof of concept, this will be a separate file
    return <div className="panel">Map and Controls (top)</div>;
}
function Controlls() { // temporary for proof of concept, this will be a separate file
    return <div className="panel">Console / Status (bottom)</div>;
}


// Additional small demo components to show top/bottom split
function ExtraPanel() {
    return <div className="panel">Extra / Logs</div>;
}
function CompactStatus() {
    return <div className="panel">Compact Status</div>;
}
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
                    <div className="sidebar-top grow-large">
                        <WarningPanel />
                        {/* place larger items here (maps, charts, large widgets) */}
                    </div>
                    <div className="sidebar-bottom grow-small">
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
