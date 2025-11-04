using Microsoft.EntityFrameworkCore;

// This is going to be our database session/gateway
// Whenever this app wants to query, insert, update, or delete data, it does it through this class
namespace MyProjectTemplate.API.Data
{
    public class AppDbContext : DbContext // Inherits from DbContext, this is the main EF Core class responsible for both managing database connections and tracking data changes
    {
        // Basically a config package that tells our EF Core how to connect to the database
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // Example table syntax we can use
        // public DbSet<RandomSensorReading> SensorReadings { get; set; }
    }
}
