using MyProjectTemplate.API.Data;
using MyProjectTemplate.API.Models;
using MyProjectTemplate.API.LifeSupportSystems;
using MyProjectTemplate.API;

namespace MyProjectTemplate.API.Services {
    public class Logger {
        private readonly AppDbContext _db;

        public Logger(AppDbContext db) {
            _db = db;
        }

        public void Log(Guid subId, Guid deviceId, string level, string message, string performedBy = "SYSTEM") {
            var log = new SubLog {
                SubId = subId,
                Level = level,
                Message = message,
                PerformedBy = performedBy,
                TimeData = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            _db.SubLogs.Add(log);
            _db.SaveChanges();
        }

        // Creating different level logs
        public void Info(Guid deviceId, string msg) => Log(Guid.Parse("11111111-1111-1111-1111-111111111111"), deviceId, "INFO", msg);
        public void Warning(Guid deviceId, string msg) => Log(Guid.Parse("11111111-1111-1111-1111-111111111111"), deviceId, "WARNING", msg);
        public void Danger(Guid deviceId, string msg) => Log(Guid.Parse("11111111-1111-1111-1111-111111111111"), deviceId, "DANGER", msg);
    }
}