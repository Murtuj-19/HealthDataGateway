using System;
using System.Collections;
using System.Collections.Generic;

namespace HealthDataGateway.Data.Models
{
    public class Hospital
    {
        public int HospitalId { get; set; }
        public string HospitalName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
        public virtual ICollection<TransferRequest> SourceTransferRequests { get; set; } = new List<TransferRequest>();
        public virtual ICollection<TransferRequest> TargetTransferRequests { get; set; } = new List<TransferRequest>();
    }
}