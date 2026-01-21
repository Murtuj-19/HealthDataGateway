using System;

namespace HealthDataGateway.Data.Models
{
    public class ActivityLog
    {
        public int LogId { get; set; }
        public string? EntityName { get; set; }
        public int? EntityId { get; set; }
        public string? Action { get; set; }
        public string? PerformedBy { get; set; }
        public DateTime LoggedAt { get; set; }
    }
}