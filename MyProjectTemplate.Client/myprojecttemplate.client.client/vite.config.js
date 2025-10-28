// Vite configuration file for the React client project.
// This file:
//  - Creates/loads HTTPS dev certs used by Vite.
//  - Configures a proxy so client requests to /weatherforecast are forwarded to the API.
//  - Exposes an alias '@' to the ./src directory.
// Edit the proxy target, ports, or cert names below as your environment requires.

import { fileURLToPath, URL } from 'node:url';

import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';
import fs from 'fs';
import path from 'path';
import child_process from 'child_process';
import { env } from 'process';

// Determine the folder where ASP.NET dev certs are stored on the developer machine.
// On Windows this is %APPDATA%/ASP.NET/https, on macOS/Linux ~/.aspnet/https
const baseFolder =
    env.APPDATA !== undefined && env.APPDATA !== ''
        ? `${env.APPDATA}/ASP.NET/https`
        : `${env.HOME}/.aspnet/https`;

// Certificate name used by Visual Studio and this project. Change if you rename the project.
const certificateName = "myprojecttemplate.client.client";
const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

// Ensure the baseFolder exists before writing certs.
if (!fs.existsSync(baseFolder)) {
    fs.mkdirSync(baseFolder, { recursive: true });
}

// If the certificate files are missing, export the dev certs using dotnet dev-certs.
// This makes HTTPS work locally and aligns with the ASP.NET Core server cert.
if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
    if (0 !== child_process.spawnSync('dotnet', [
        'dev-certs',
        'https',
        '--export-path',
        certFilePath,
        '--format',
        'Pem',
        '--no-password',
    ], { stdio: 'inherit', }).status) {
        throw new Error("Could not create certificate.");
    }
}

// Proxy target selection logic:
// - If ASPNETCORE_HTTPS_PORT is set (common when SPA proxying is configured), use it.
// - Otherwise fall back to ASPNETCORE_URLS or a hard-coded API HTTPS URL.
// Update the fallback if your API uses a different port.
const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
    env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:7048';

// Export Vite config. Typical adjustments:
// - Change server.port to use a different local port.
// - Add additional proxy entries for other API endpoints.
// - Add more plugins for typescript, SWC, or other transforms.
export default defineConfig({
    plugins: [plugin()],
    resolve: {
        alias: {
            // Use '@/...' to import from src
            '@': fileURLToPath(new URL('./src', import.meta.url))
        }
    },
    server: {
        proxy: {
            // Forward requests that start with /weatherforecast to the API target.
            '^/weatherforecast': {
                target,
                secure: false // disable SSL verification for local dev certs
            }
        },
        // Default Vite dev port for this template. Override by setting DEV_SERVER_PORT in env.
        port: parseInt(env.DEV_SERVER_PORT || '60773'),
        https: {
            // Read the certificate exported by dotnet dev-certs so browser doesn't complain.
            key: fs.readFileSync(keyFilePath),
            cert: fs.readFileSync(certFilePath),
        }
    }
});