# Solution comments and guidance (template overview)

This document summarizes the purpose of files in the solution and where to change/expand things.
It is intended as a developer-friendly guide to get started and to extend the template.

## Projects
- `MyProjectTemplate.API` (ASP.NET Core Web API)
  - `Program.cs` — App startup: registers controllers, Swagger, and CORS. Update origins and environment-specific behavior here.
    - Visual Studio launch profiles are in `Properties/launchSettings.json`.
    - To change ports, edit the launch settings or set `__ASPNETCORE_URLS__` / `__ASPNETCORE_HTTPS_PORT__`.
    - To trust local certs run: `__dotnet dev-certs https --trust__`.
  - `Controllers/WeatherForecastController.cs` — Example controller returning sample data. Replace with real controllers and services.
  - `WeatherForecast.cs` — Example model (POCO). Replace/extend with real DTOs/Entities.
  - Expansion ideas:
    - Add persistence with EF Core, repository/service layers, DTO mapping (AutoMapper).
    - Add Authentication/Authorization (JWT, Identity).
    - Add health checks (`AddHealthChecks()`), logging sinks, and metrics.

- `MyProjectTemplate.Client` (React + Vite)
  - `vite.config.js` — Dev server configuration, HTTPS cert loading, and API proxy rules.
    - Proxy forwards `/weatherforecast` to the API. Update or add proxies for other endpoints.
    - Use `DEV_SERVER_PORT` env var to change client port or edit `port` directly.
  - `index.html` + `web.jsx` — Vite entry and React mount point. Rename to `.tsx`/`.ts` when migrating to TypeScript.
  - `src/App.jsx`, `src/MyComponent.jsx` — Example UI and API call.
    - Replace `fetch` with `axios` or data libraries (React Query) for production apps.
  - `package.json`
    - `scripts.dev` runs Vite: `npm run dev`. If Visual Studio uses SPA proxy, ensure `SpaProxyLaunchCommand` matches this script.
    - Add build scripts for production or CI/CD.
  - Expansion ideas:
    - Add routing (React Router), state management (Redux, Zustand, Recoil), styling system (Tailwind, CSS modules).
    - Add tests (Jest, Vitest, React Testing Library).
    - Add TypeScript: rename `.jsx` -> `.tsx`, install types, adjust Vite config.

## Local development flow
- Trust local certs once: `dotnet dev-certs https --trust`.
- Start API (Visual Studio or `dotnet run` in project): confirm `https://localhost:7048` responds.
- Start client: `npm install` (first time) then `npm run dev`. The Vite server uses the HTTPS cert and proxies API calls.
- Visual Studio SPA proxy:
  - The server project can be configured to run the SPA via the `SpaProxyLaunchCommand` in the server `.csproj` (e.g., `npm run dev`).
  - If using the SPA proxy, the server will set env vars so Vite knows the API target automatically.

## Notes about security & production
- CORS should be narrowly scoped and not allow arbitrary origins in production.
- Do not use dev certs in production — use real certificates from a CA or a managed service.
- Remove or restrict Swagger UI in production, or use authentication to protect it.

## Useful Visual Studio settings and commands
- Edit launch profiles: `Properties/launchSettings.json` or use the __Launch Profiles__ UI.
- Start debugging: __Debug > Start Debugging__ or press F5.
- To run server and client together from Visual Studio, ensure the server project has the SPA proxy configured in its `.csproj` (`SpaProxyLaunchCommand`) and set the solution startup project to the server.

---

If you want, I can:
- Sweep the repository and add comments to any other files you care about.
- Convert the client to TypeScript and add recommended dev dependencies.
- Add a README in the API explaining deployment and production considerations.