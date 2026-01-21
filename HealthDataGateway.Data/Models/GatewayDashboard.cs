using System;

namespace HealthDataGateway.Data.Models
{
    public class GatewayDashboard
    {
        public int TotalRequests { get; set; }
        public int SentRequests { get; set; }
        public int CancelledRequests { get; set; }
    }
}
