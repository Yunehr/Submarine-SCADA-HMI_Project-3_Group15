using Microsoft.AspNetCore.Mvc;
using MyProjectTemplate.API.Controllers;
// Program.cs - Application startup for the API project.
// This file configures services (MVC controllers, Swagger, CORS) and the request pipeline.
// Keep this file minimal in templates; move heavier configuration to extension methods in larger apps.

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register MVC controllers (attribute routed controllers live under Controllers/)
builder.Services.AddControllers();

// Add OpenAPI/Swagger generation for development and testing.
// In production you may want to restrict or disable the swagger endpoint.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS configuration:
// - This sample adds a named policy "AllowReactApp" that whitelists origins used by the local client (Vite).
// - Update the origins below to match the URLs your client will run on.
// - In production, prefer more restrictive policies (specific origins + credentials as needed).
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins(
                    "https://localhost:60773", // Vite dev server origin
                    "http://localhost:60773",  // if you ever run without https
                    "https://localhost:7048",  // API HTTPS url
                    "http://localhost:5225"    // API HTTP url (launchSettings)
                )
                .AllowAnyHeader()
                .AllowAnyMethod();
            // .AllowCredentials() // add if you need cookies/Windows auth between client and API

        });
});

var app = builder.Build();

// Development-only middleware: show Swagger UI and OpenAPI docs.
// Keep these inside the IsDevelopment check to avoid exposing API docs in production.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Use CORS before controllers
app.UseCors("AllowReactApp");

// Map controller routes(e.g., WeatherForecastControll
app.MapControllers();

app.Run();

// Notes / Where to update:
// - Ports & URLs: see Properties/launchSettings.json. You can alter those or set environment variables
//   like __ASPNETCORE_URLS__ or __ASPNETCORE_HTTPS_PORT__ when launching.
// - To trust the local certificate used by Vite & ASP.NET Core, run: __dotnet dev-certs https --trust__
// - If you want the SPA to be launched automatically by the server, see the client project __SpaProxyLaunchCommand__
//   setting in the client server project (.csproj) and the 