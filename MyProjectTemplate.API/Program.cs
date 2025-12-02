using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Remember to press tools --> NuGet Package Manager --> Package Manager Console --> type the below
using MyProjectTemplate.API.Controllers;
using MyProjectTemplate.API.Services;
using MyProjectTemplate.API.Data;   // Install-Package Microsoft.EntityFrameworkCore.Tools
                                    // Search for sqlite efcore.sqlite and efcore.sqlite.core and download
using MyProjectTemplate.API;
using System;
using System.Threading;
using MyProjectTemplate.API.LifeSupportSystems;
using MyProjectTemplate.API.Models;

// Program.cs - Application startup for the API project.
// This file configures services (MVC controllers, Swagger, CORS) and the request pipeline.
// Keep this file minimal in templates; move heavier configuration to extension methods in larger apps.
var builder = WebApplication.CreateBuilder(args);

// ----------------------------------------------------------------------------
// CONFIGURE OPTIONS
// ----------------------------------------------------------------------------

builder.Services.Configure<DeviceThresholds>(
    builder.Configuration.GetSection("DeviceThresholds")); // This gets the device thresholds from the appsettings.json

// ----------------------------------------------------------------------------
// CONTROLLERS + BASIC SERVICES
// ----------------------------------------------------------------------------

builder.Services.AddControllers();

// Register the Device Logger as a singleton so it can be used throughout the whole app
builder.Services.AddScoped<Logger>();
builder.Services.AddScoped<DeviceLoggingService>();
builder.Services.AddScoped<ThresholdsHandlers>();

// ----------------------------------------------------------------------------
// DATABASE
// ----------------------------------------------------------------------------

builder.Services.AddDbContext<AppDbContext>(options => {
    // This is how we can use the database password variable instead of committing it to GitHub
    var baseConn = builder.Configuration.GetConnectionString("DefaultConnection");

    // This line auto configures SQLite file
    options.UseSqlite(baseConn);
});

// ----------------------------------------------------------------------------
// SWAGGER
// ----------------------------------------------------------------------------

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ----------------------------------------------------------------------------
// CORS
// ----------------------------------------------------------------------------

builder.Services.AddCors(options => {
    options.AddPolicy("AllowReactApp",
        policy => {
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

// ----------------------------------------------------------------------------
// EVENT BUS (register service, NOT usage yet)
// ----------------------------------------------------------------------------

builder.Services.AddSingleton<IEventBus, EventBus>();   // replaces var bus = newEventBus();

// ----------------------------------------------------------------------------
// LIFE-SUPPORT CONTROLLER + DICTIONARIES
// ----------------------------------------------------------------------------

// These will be created AFTER monitors are created (below), but registered now
builder.Services.AddScoped<LifeSupportController>();

builder.Services.AddSingleton<Dictionary<Guid, string>>();
builder.Services.AddSingleton<Dictionary<string, IDevice>>();

// ----------------------------------------------------------------------------
// BUILD APP (everything above must run BEFORE this)
// ----------------------------------------------------------------------------

var app = builder.Build();

// ----------------------------------------------------------------------------
// EVENT BUS INSTANCE
// ----------------------------------------------------------------------------

var bus = app.Services.GetRequiredService<IEventBus>();

// ----------------------------------------------------------------------------
// MONITOR INITIALIZATION
// ----------------------------------------------------------------------------

var o2 = new OxygenMonitor();
var co2 = new Co2Monitor();
var air = new AirReserveMonitor();
var intPressure = new PressureMonitor();
var exPressure = new PressureMonitor();
var temperature = new TemperatureMonitor();
var humidity = new HumidityMonitor();
var reactorOutput = new ReactorOutputMonitor();
var coolantMonitor = new CoolantMonitor();
var fuelRodMonitor = new FuelRodMonitor();
var radiationMonitor = new RadMonitor();
var batteryMonitor = new BatteryMonitor();
var reactorTemp = new TemperatureMonitor();

var areaNames = new Dictionary<Guid, string>
{
    [o2.Id] = "O2 Main Cabin",
    [co2.Id] = "CO2 Main Cabin",
    [air.Id] = "Air Reserve Tank",
    [intPressure.Id] = "Internal Pressure",
    [exPressure.Id] = "External Pressure",
    [temperature.Id] = "Main Cabin Temperature",
    [humidity.Id] = "Main Cabin Humidity",
    [reactorOutput.Id] = "Reactor Output",
    [coolantMonitor.Id] = "Reactor Coolant Level",
    [fuelRodMonitor.Id] = "Reactor Fuel Rod Integrity",
    [radiationMonitor.Id] = "Reactor Radiation Level",
    [batteryMonitor.Id] = "Battery Charge Level",
    [reactorTemp.Id] = "Reactor Temperature"
};

var devices = new Dictionary<string, IDevice>
{
    ["O2"] = o2,
    ["CO2"] = co2,
    ["Air"] = air,
    ["IntPressure"] = intPressure,
    ["ExPressure"] = exPressure,
    ["Temperature"] = temperature,
    ["Humidity"] = humidity,
    ["ReactorOutput"] = reactorOutput,
    ["Coolant"] = coolantMonitor,
    ["FuelRod"] = fuelRodMonitor,
    ["Radiation"] = radiationMonitor,
    ["Battery"] = batteryMonitor,
    ["ReactorTemp"] = reactorTemp
};

builder.Services.AddSingleton(areaNames);
builder.Services.AddSingleton(devices);


var app = builder.Build();

// --- EventBus + monitors: set these up BEFORE app.Run() ---

var bus = app.Services.GetRequiredService<IEventBus>();



bus.Register(o2);
bus.Register(co2);
bus.Register(air);
bus.Register(intPressure);
bus.Register(exPressure);
bus.Register(temperature);
bus.Register(humidity);
bus.Register(reactorOutput);
bus.Register(coolantMonitor);
bus.Register(fuelRodMonitor);
bus.Register(radiationMonitor);
bus.Register(batteryMonitor);
bus.Register(reactorTemp);

// ----------------------------------------------------------------------------
// FORCE SUB INTO DATABASE (one-time)
// ----------------------------------------------------------------------------

using (var scope = app.Services.CreateScope()) {
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Pick a fixed SubId so readings can reference it
    var subId = Guid.Parse("11111111-1111-1111-1111-111111111111");

    // Check if it already exists
    if (!db.SubData.Any(s => s.SubId == subId)) {
        var sub = new SubDatum {
            SubId = subId,
            SubName = "USS Neptune"
        };

        db.SubData.Add(sub);
        db.SaveChanges();
        Console.WriteLine("SubDatum added!");
    }
    else {
        Console.WriteLine("SubDatum already exists.");
    }
}

// ----------------------------------------------------------------------------
// LOGGING SUBSCRIPTIONS
// ----------------------------------------------------------------------------

foreach (DeviceType type in Enum.GetValues<DeviceType>()) {
    bus.Subscribe(type, reading => {
        using var scope = app.Services.CreateScope();
        var alerts = scope.ServiceProvider.GetRequiredService<DeviceLoggingService>();
        
        alerts.HandleReading(reading);
    });
}

// ----------------------------------------------------------------------------
// BUILD DICTIONARIES (NEEDED BY LifeSupportController)
// ----------------------------------------------------------------------------

var areaNames = new Dictionary<Guid, string> {
    [o2.Id] = "O2 Main Cabin",
    [co2.Id] = "CO2 Main Cabin",
    [air.Id] = "Air Reserve Tank",
    [intPressure.Id] = "Internal Pressure",
    [exPressure.Id] = "External Pressure",
    [temperature.Id] = "Main Cabin Temperature",
    [humidity.Id] = "Main Cabin Humidity"
};

var devices = new Dictionary<string, IDevice> {
    ["O2"] = o2,
    ["Co2"] = co2,
    ["Air"] = air,
    ["IntPressure"] = intPressure,
    ["ExPressure"] = exPressure,
    ["Temperature"] = temperature,
    ["Humidity"] = humidity
};

// ----------------------------------------------------------------------------
// LIFE SUPPORT SUBSCRIPTIONS
// ----------------------------------------------------------------------------

using (var scope = app.Services.CreateScope()) {
    var lifeSupportController = scope.ServiceProvider.GetRequiredService<LifeSupportController>();
    lifeSupportController.SetupSubscriptions();
}

Console.WriteLine("Monitors started. API is starting...");

// ----------------------------------------------------------------------------
// MIDDLEWARE + ROUTING
// ----------------------------------------------------------------------------

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Use CORS before controllers
app.UseCors("AllowReactApp");

// Map controller routes(e.g., WeatherForecastControll
app.MapControllers();

Console.WriteLine("Monitors started. API is starting...");

app.Run();

// Notes / Where to update:
// - Ports & URLs: see Properties/launchSettings.json. You can alter those or set environment variables
//   like __ASPNETCORE_URLS__ or __ASPNETCORE_HTTPS_PORT__ when launching.
// - To trust the local certificate used by Vite & ASP.NET Core, run: __dotnet dev-certs https --trust__
// - If you want the SPA to be launched automatically by the server, see the client project __SpaProxyLaunchCommand__
//   setting in the client server project (.csproj) and the 
