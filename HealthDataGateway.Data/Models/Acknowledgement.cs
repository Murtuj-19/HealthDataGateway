using System;

namespace HealthDataGateway.Data.Models
{
    public class Acknowledgement
    {
        public int AcknowledgementId { get; set; }
        public int IncomingRequestId { get; set; }
        public string? Decision { get; set; }
        public string? Remarks { get; set; }
        public DateTime RespondedAt { get; set; }

        public virtual IncomingTransferRequest IncomingTransferRequest { get; set; } = null!;
    }
}