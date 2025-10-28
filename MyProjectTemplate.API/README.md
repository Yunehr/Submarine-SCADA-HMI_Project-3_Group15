# MyProjectTemplate.API

Small ASP.NET Core Web API template (.NET 8).  
Includes a sample `WeatherForecast` controller, Swagger/OpenAPI, and a CORS policy used for local development with a Vite-based React client.

## Contents
- `Program.cs` — app startup, services (controllers, Swagger, CORS), and middleware pipeline.
- `Controllers/WeatherForecastController.cs` — example endpoint `GET /WeatherForecast`.
- `WeatherForecast.cs` — sample model.

## Prerequisites
- .NET 8 SDK installed
- (Optional for local client integration) Node.js + npm
- Recommended: Visual Studio 2022 or later (or `dotnet` CLI)

## Local development

1. Trust local dev certificate (required for HTTPS between Vite and ASP.NET Core):
   - Run: `dotnet dev-certs https --trust`
   - On Windows this will prompt a trust dialog.

2. Start the API
   - From Visual Studio: select the API project and press F5 (or use __Debug > Start Debugging__).
     - Launch profiles are defined in `Properties/launchSettings.json`.
   - From the command line:
     - cd to `MyProjectTemplate.API`
     - `dotnet run`
   - Swagger UI is available in Development at: `https://localhost:7048/swagger` (ports are determined by launch settings).

3. Start the client (optional)
   - The React client (Vite) lives in `../myprojecttemplate.client.client`.
   - In that folder:
     - `npm install`
     - `npm run dev`
   - The client dev server (Vite) proxies `/weatherforecast` to the API when configured in `vite.config.js`.
   - The server project can also launch the client automatically if its `.csproj` property __SpaProxyLaunchCommand__ matches the client script (e.g., `npm run dev`).

## Important configuration points

- Ports & URLs
  - Launch URLs are in `Properties/launchSettings.json`.
  - You can also set environment variables such as __ASPNETCORE_URLS__ or __ASPNETCORE_HTTPS_PORT__ to change the API address used by the client proxy.

- CORS
  - A named policy `AllowReactApp` is configured in `Program.cs`. Update allowed origins there.
  - For production, do not use permissive CORS policies — restrict to exact origins and enable credentials only when necessary.

- HTTPS certs
  - The client Vite server and the API share the local dev cert exported by `dotnet dev-certs`. If the browser complains about certificate trust, ensure you ran the trust command.

## Endpoints
- GET /WeatherForecast — returns an array of `WeatherForecast` objects (sample data).
- Open the interactive API docs at `/swagger` in Development.

## Troubleshooting

- `ECONNREFUSED` when the client tries `/weatherforecast`:
  - Ensure the API is running and reachable at the configured HTTPS port (check `launchSettings.json`).
  - Confirm `vite.config.js` proxy `target` matches the API URL (or let __ASPNETCORE_HTTPS_PORT__ be set by the SPA proxy).

- CORS errors:
  - Add the client origin to the `WithOrigins(...)` list in `Program.cs` or use a development-only permissive policy while debugging.

- Browser cert warnings:
  - Re-run `dotnet dev-certs https --trust` and restart the dev servers.

## How to expand this template

- Data & persistence:
  - Add EF Core, migrations, database contexts, repository/service layers.
- Security:
  - Add authentication (JWT, Identity), authorization policies, and secure Swagger in non-dev environments.
- Observability:
  - Add structured logging (serilog), metrics, and health checks with `AddHealthChecks()`.
- Client improvements:
  - Use React Router, React Query / SWR, TypeScript (`.tsx`), and a styling system (Tailwind, CSS modules).
- Production deployment:
  - Use real TLS certificates, environment-specific appsettings, containerize with Docker, or deploy to Azure App Service / Azure Container Apps.

## CI / Release notes
- Build and publish:
  - `dotnet publish -c Release -o ./publish`
- Docker:
  - Add a `Dockerfile` and build/push an image; prefer multi-stage builds for smaller artifacts.

## References
- ASP.NET Core docs: https://learn.microsoft.com/aspnet/core
- dotnet dev-certs: `dotnet dev-certs https --trust`

---

If you want, I can:
- Add a `README.md` file to the client folder with matching instructions.
- Add Dockerfile and example GitHub Actions workflow for CI/CD.
- Generate template stubs for EF Core and authentication integration.