using System;
using System.Collections;
using System.Collections.Generic;

namespace HealthDataGateway.Data.Models
{
    public class IncomingTransferRequest
    {
        public int IncomingRequestId { get; set; }
        public int ConnectorRequestId { get; set; }
        public int SourceHospitalId { get; set; }
        public int TargetHospitalId { get; set; }
        public string? IncomingStatus { get; set; }
        public DateTime ReceivedAt { get; set; }

        public virtual ConnectorRequest? ConnectorRequest { get; set; }
        public virtual Hospital? SourceHospital { get; set; }
        public virtual Hospital? TargetHospital { get; set; }
        public virtual ICollection<Acknowledgement>? Acknowledgements { get; set; } = new List<Acknowledgement>();
    }
}