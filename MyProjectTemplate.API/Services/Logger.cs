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

        public void Log(Guid subId, string level, string message, string performedBy = "SYSTEM") {
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
        public void Info(Guid id, string msg) => Log(id, "INFO", msg);
        public void Warning(Guid id, string msg) => Log(id, "WARNING", msg);
        public void Danger(Guid id, string msg) => Log(id, "DANGER", msg);
    }
}