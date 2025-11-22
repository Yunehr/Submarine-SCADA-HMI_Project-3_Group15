using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Remember to press tools --> NuGet Package Manager --> Package Manager Console --> type the below
using MyProjectTemplate.API.Controllers;
using MyProjectTemplate.API.Data;   // Install-Package Microsoft.EntityFrameworkCore.Tools
                                    // Search for sqlite efcore.sqlite and efcore.sqlite.core and download
using MyProjectTemplate.API;
using System;
using System.Threading;

// Program.cs - Application startup for the API project.
// This file configures services (MVC controllers, Swagger, CORS) and the request pipeline.
// Keep this file minimal in templates; move heavier configuration to extension methods in larger apps.

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Register MVC controllers (attribute routed controllers live under Controllers/)
builder.Services.AddControllers();


// Add EF Core + SQlite
// This is how we will tell the ASP.NET Core dependency injection (DI) system: “Whenever something in our app asks for an AppDbContext, create one for it automatically.”
builder.Services.AddDbContext<AppDbContext>(options =>
{
    // This is how we can use the database password variable instead of committing it to GitHub
    var baseConn = builder.Configuration.GetConnectionString("DefaultConnection");

    // This line auto configures SQLite file
    options.UseSqlite(baseConn);
});

// Add OpenAPI/Swagger generation for development and testing.
// In production you may want to restrict or disable the swagger endpoint.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registers EventBus as a singleton so controllers can use it
builder.Services.AddSingleton<IEventBus, EventBus>();   // replaces var bus = newEventBus();


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

// --- EventBus + monitors: set these up BEFORE app.Run() ---

var bus = app.Services.GetRequiredService<IEventBus>();

var o2 = new OxygenMonitor();
var co2 = new Co2Monitor();
var air = new AirReserveMonitor();
var pressure = new PressureMonitor();
var temperature = new TemperatureMonitor();
var humidity = new HumidityMonitor();

bus.Register(o2);
bus.Register(co2);
bus.Register(air);
bus.Register(pressure);
bus.Register(temperature);
bus.Register(humidity);

// Alarm thresholds
const double O2_MIN = 21.0;
const double CO2_MAX = 390;
const double AIR_RESERVE_MIN = 40.0;
const double PRESSURE_MAX = 1.2;
const double PRESSURE_MIN = 0.8;
const double TEMP_MAX = 27.0;
const double TEMP_MIN = 15.0;
const double HUMIDITY_MAX = 60.0;
const double HUMIDITY_MIN = 20.0;

Console.WriteLine("Monitors started. API is starting...");

// Subscribe
bus.Subscribe(DeviceType.Oxygen, reading =>
{
    Console.WriteLine($"O₂: {reading.Value:F2} {reading.Unit}");
    if (reading.Value < O2_MIN)
        Console.WriteLine("OXYGEN ALARM!");
});

bus.Subscribe(DeviceType.CO2, reading =>
{
    Console.WriteLine($"CO₂: {reading.Value:F0} {reading.Unit}");
    if (reading.Value > CO2_MAX)
        Console.WriteLine("CO₂ ALARM!");
});

bus.Subscribe(DeviceType.AirReserve, reading =>  // Currently in Air Reserve, the value can go greater than 100%. In every UI test I have run so far I cannot see any value changes due to AirReserve being > 100
{
    Console.WriteLine($"Air Reserve: {reading.Value:F2} {reading.Unit}");
    if (reading.Value < AIR_RESERVE_MIN)
        Console.WriteLine("AIR RESERVE ALARM!");
});

bus.Subscribe(DeviceType.Pressure, reading =>
{
    Console.WriteLine($"Pressure: {reading.Value:F2} {reading.Unit}");
    if (reading.Value > PRESSURE_MAX || reading.Value < PRESSURE_MIN)
        Console.WriteLine("PRESSURE ALARM!");
});

bus.Subscribe(DeviceType.Temperature, reading =>
{
    Console.WriteLine($"Temperature: {reading.Value:F2} {reading.Unit}");
    if (reading.Value > TEMP_MAX || reading.Value < TEMP_MIN)
        Console.WriteLine("TEMPERATURE ALARM!");
});

bus.Subscribe(DeviceType.Humidity, reading =>
{
    Console.WriteLine($"Humidity: {reading.Value:F2} {reading.Unit}");
    if (reading.Value > HUMIDITY_MAX || reading.Value < HUMIDITY_MIN)
        Console.WriteLine("HUMIDITY ALARM!");
});

// If your monitors have a Start() method, call it here:
o2.Start();
co2.Start();
air.Start();
pressure.Start();
temperature.Start();
humidity.Start();

Console.WriteLine("Monitors started. API is starting...");

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