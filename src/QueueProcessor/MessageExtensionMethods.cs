using Amazon.Lambda.SQSEvents;

namespace QueueProcessor
{
    public static class MessageExtensionMethods
    {
        public static bool TryGetAttribute(this SQSEvent.SQSMessage message, string key, out string value)
        {
            if (message.MessageAttributes.TryGetValue(key, out var attribute) &&
                !string.IsNullOrEmpty(attribute.StringValue))
            {
                value = attribute.StringValue;
                return true;
            }
            value = null;
            return false;
        }
    }
}
