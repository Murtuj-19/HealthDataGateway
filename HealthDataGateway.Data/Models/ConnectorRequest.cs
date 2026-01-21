using System;
using System.Collections;
using System.Collections.Generic;

namespace HealthDataGateway.Data.Models
{
    public class ConnectorRequest
    {
        public int ConnectorRequestId { get; set; }
        public int TransferRequestId { get; set; }
        public byte[]? EncryptedPayload { get; set; }
        public string? EncryptionType { get; set; }
        public string? AuthType { get; set; }
        public bool IsFHIRCompliant { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual TransferRequest? TransferRequest { get; set; }
        public virtual ICollection<IncomingTransferRequest>? IncomingTransferRequests { get; set; } = new List<IncomingTransferRequest>();
    }
}