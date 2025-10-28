// Top-level React component for the client app.
// Keep this component small in templates; split layout, routes, and pages as the app grows.

import React from 'react';
import MyComponent from './MyComponent';
import './App.css'; // Example: add app-level styles here.

export default function App() {
    return (
        <div style={{ minHeight: '100vh', display: 'flex', flexDirection: 'column' }}>
            <header style={{ padding: '1rem', background: '#282c34', color: 'white' }}>
                <h1>MyProjectTemplate Client</h1>
            </header>
            <main style={{ flex: 1, padding: '1rem' }}>
                {/* Replace MyComponent with routes or additional UI as needed */}
                <MyComponent />
            </main>
        </div>
    );
}