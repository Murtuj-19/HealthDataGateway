namespace HealthDataGateway.Services.Constants
{
    public static class Statuses
    {
        public static class Transfer
        {
            public const string Created = "CREATED";
            public const string Queued = "QUEUED";
            public const string Sent = "SENT";
            public const string Accepted = "ACCEPTED";
            public const string Cancelled = "CANCELLED";
            public const string Rejected = "REJECTED";
        }

        public static class Connector
        {
            public const string Processing = "PROCESSING";
            public const string Delivered = "DELIVERED";
            public const string Failed = "FAILED";
        }

        public static class Incoming
        {
            public const string Pending = "PENDING";
            public const string Accepted = "ACCEPTED";
            public const string Rejected = "REJECTED";
        }
    }
}
