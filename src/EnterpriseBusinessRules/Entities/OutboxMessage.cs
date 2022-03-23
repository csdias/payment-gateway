using System;
using System.Collections.Generic;

namespace EnterpriseBusinessRules.Sns.Entities
{
    public class OutboxMessage
    {
        public Guid MessageId { get; set; }
        public string Payload { get; set; }
        public string TopicArn { get; set; }
        public string Event { get; set; }
        public ICollection<MessageFilter> MessageFilters { get; set; }

        public DateTimeOffset OccurredAt { get; set; }
        public DateTimeOffset? PublishedAt { get; set; }
        public DateTimeOffset LastUpdated { get; set; }

    }
}
