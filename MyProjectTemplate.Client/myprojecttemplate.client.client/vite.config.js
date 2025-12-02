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
        server: {
            proxy: {
                '/api': 'https://localhost:7048'  //  backend port
            },
            port: 60773, //  frontend port
            https: {
                key: fs.readFileSync(keyFilePath),
                cert: fs.readFileSync(certFilePath),
            }
        }

    }
});