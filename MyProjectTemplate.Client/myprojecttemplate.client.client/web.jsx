// Vite web entry point.
// Mounts the React app into the DOM element with id "react-root" (defined in index.html).
// If you convert this project to TypeScript, rename to web.tsx.

import React from 'react';
import { createRoot } from 'react-dom/client';
import App from './src/App';

// Find the mount point declared in index.html
const rootEl = document.getElementById('react-root');
if (!rootEl) {
    throw new Error('Could not find #react-root in index.html');
}

// Use the new createRoot API for React 18+.
const root = createRoot(rootEl);
root.render(
    <React.StrictMode>
        <App />
    </React.StrictMode>
);