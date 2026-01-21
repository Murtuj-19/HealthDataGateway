using System;
using System.Collections;
using System.Collections.Generic;

namespace HealthDataGateway.Data.Models
{
    public class TransferRequest
    {
        public int TransferRequestId { get; set; }
        public int SourceHospitalId { get; set; }
        public int PatientId { get; set; }
        public int TargetHospitalId { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Hospital? SourceHospital { get; set; }
        public virtual Hospital? TargetHospital { get; set; }
        public virtual Patient? Patient { get; set; }
        public virtual ICollection<ConnectorRequest>? ConnectorRequests { get; set; } = new List<ConnectorRequest>();
    }
}