using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyProjectTemplate.API.Eventbus {
    public interface IEventbus {
        void Publish(double value, string unit, string type);
        void Subscribe(string type, Action<double> handler);

         // Action<X> handler --> This means give me a function that accepts an X.
        // “bus.Subscribe("O2", controller.HandleOxygen); -----> Event bus: when O2 changes, call this phone number.”
    }
}

// Example on how we can use this jawn (very basically)
/*
    var bus = new Eventbus();                  // Create the event bus object 

    var o2Device = new Device(bus, "O2");    // Create the devices, connecting them to the bus...
    var co2Device = new Device(bus, "CO2"); // AND what they will be notified of: Device(IEventBus bus, string type)

    var controller = new Controller();    // Create the controller next
    controller.SubscribeTo(bus, "O2");   // Subscribe it to the the bus again, and for the thing to get notified about: SubscribeTo(IEventBus bus, string topic)
    controller.SubscribeTo(bus, "CO2"); // SubscribeTo does this --> bus.Subscribe(topic ("O2"), OnMessage (Function of the controller);

    // Devices publish new values:
    o2Device.Device_Event(20.9);     // Sending device events (just the change in value (each device has the type already)
    co2Device.Device_Event(0.04);

    // Later:
    o2Device.Device_Event(19.8);

*/