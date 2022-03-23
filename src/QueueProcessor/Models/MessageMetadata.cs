using System;

namespace QueueProcessor.Models
{
    public class MessageMetadata
    {
        public string MessageName { get; set; }
        public string MessageVersion { get; set; }
        public string ContextId { get; set; }
        public DateTimeOffset OccurredAt { get; set; }
    }
}
