using System;

namespace EnterpriseBusinessRules.Sns.Entities
{
    public class MessageFilter
    {
        public Guid MessageId { get; set; }
        public string FilterKey { get; set; }
        public string FilterValue { get; set; }
    }
}
