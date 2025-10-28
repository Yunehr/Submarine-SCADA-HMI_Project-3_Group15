// Small example component that calls the API endpoint GET /weatherforecast and renders results.
// This demonstrates fetch usage, loading/error state, and basic rendering.
// Expand by using a proper data-layer (React Query, SWR, or custom hooks) for caching and retries.

import React, { useEffect, useState } from 'react';

export default function MyComponent() {
    const [items, setItems] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        let mounted = true;
        // The Vite dev server proxies /weatherforecast to the API (see vite.config.js).
        fetch('/weatherforecast')
            .then((res) => {
                if (!res.ok) throw new Error(`HTTP ${res.status}`);
                return res.json();
            })
            .then((json) => mounted && setItems(json))
            .catch((err) => mounted && setError(err.message))
            .finally(() => mounted && setLoading(false));

        // Cleanup pattern prevents setting state after unmount
        return () => {
            mounted = false;
        };
    }, []);

    if (loading) return <div>Loading...</div>;
    if (error) return <div style={{ color: 'red' }}>Error: {error}</div>;
    if (!items || items.length === 0) return <div>No data</div>;

    return (
        <div>
            <ul style={{ listStyle: 'none', padding: 0 }}>
                {items.map((it, i) => {
                    const date = it.date ? new Date(it.date).toLocaleDateString() : `#${i}`;
                    return (
                        <li key={i} style={{ padding: '0.5rem 0', borderBottom: '1px solid #eee' }}>
                            <div style={{ fontWeight: 600 }}>{date}</div>
                            <div>{it.summary} — {it.temperatureC}°C</div>
                        </li>
                    );
                })}
            </ul>
        </div>
    );
}