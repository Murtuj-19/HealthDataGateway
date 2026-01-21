using System;

namespace HealthDataGateway.Data.Models
{
    public class Diagnosis
    {
        public int DiagnosisId { get; set; }
        public int PatientId { get; set; }
        public string? DiagnosisText { get; set; }
        public DateTime DiagnosedAt { get; set; }

        public virtual Patient Patient { get; set; } = null!;
    }
}