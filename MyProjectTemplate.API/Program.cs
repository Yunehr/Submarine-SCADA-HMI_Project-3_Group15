using Microsoft.AspNetCore.Mvc;
using MyProjectTemplate.API.Controllers;

using Microsoft.EntityFrameworkCore; // Remember to press tools --> NuGet Package Manager --> Package Manager Console --> type the below
using MyProjectTemplate.API.Data;   // Install-Package Microsoft.EntityFrameworkCore.Tools
                                   // Install - Package Pomelo.EntityFrameworkCore.MySql - Version 9.0.0

// Program.cs - Application startup for the API project.
// This file configures services (MVC controllers, Swagger, CORS) and the request pipeline.
// Keep this file minimal in templates; move heavier configuration to extension methods in larger apps.

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Register MVC controllers (attribute routed controllers live under Controllers/)
builder.Services.AddControllers();


// This is how we can use the database password variable instead of committing it to GitHub
var baseConn = builder.Configuration.GetConnectionString("DefaultConnection");
var password = Environment.GetEnvironmentVariable("DB_PASSWORD"); // The powershell command from the json comment

if (string.IsNullOrEmpty(password))
{
    throw new Exception("Missing password in the environment variable DB_PASSWORD"); // remember to do the step listed in the appsetting.json (ask for the password)
}

// building the final connection string
var conString = $"{baseConn}password={password};";

// Add EF Core + MySQL
// This is how we will tell the ASP.NET Core dependency injection (DI) system: “Whenever something in our app asks for an AppDbContext, create one for it automatically.”

builder.Services.AddDbContext<AppDbContext>(options =>
{
    // This line auto configures MySQL database given the connection string 
    options.UseMySql(conString, ServerVersion.AutoDetect(conString)); // The conString already fetches our connection string from appsettings.json
});

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