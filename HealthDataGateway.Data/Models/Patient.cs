using System;
using System.Collections;
using System.Collections.Generic;

namespace HealthDataGateway.Data.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public int HospitalId { get; set; }
        public string LocalPatientId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime? DOB { get; set; }
        public string? Gender { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Hospital Hospital { get; set; } = null!;
        public virtual ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();
        public virtual ICollection<TransferRequest> TransferRequests { get; set; } = new List<TransferRequest>();
    }
}