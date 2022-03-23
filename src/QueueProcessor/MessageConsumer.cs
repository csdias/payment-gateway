using Amazon.Lambda.SQSEvents;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Threading.Tasks;
using QueueProcessor.MessageTracker;
using QueueProcessor.Models;

namespace QueueProcessor
{
    public class MessageConsumer
    {
        private readonly ITrackerRepository _trackerRepository;
        private readonly Func<string, IEventHandler> _eventHandlerFactory;


        public MessageConsumer(ITrackerRepository trackerRepository, Func<string, IEventHandler> eventHandlerFactory)
        {
            _trackerRepository = trackerRepository;
            _eventHandlerFactory = eventHandlerFactory;
        }

        private IEventHandler ValidateAndGetMessageHandler(string eventName)
        {
            switch (eventName)
            {
                case EventNames.PaymentOrderEvent:
                    return _eventHandlerFactory(eventName);
                default:
                    return null;
            }
        }

        private static JObject ValidateAndExtractPayload(SQSEvent.SQSMessage sqsMessage)
        {
            var snsMessage = JObject.Parse(sqsMessage.Body);
            var messagePayload = JObject.Parse(snsMessage?.SelectToken("Payload")?.ToString());
            
            return messagePayload;
        }

        private static MessageMetadata ValidateAndExtractMetadata(SQSEvent.SQSMessage sqsMessage, out string messageName)
        {
            var snsMessage = JObject.Parse(sqsMessage.Body);

            var messageMetadata = JObject.Parse(snsMessage?.SelectToken("Metadata")?.ToString());
            messageName = messageMetadata.GetValue("MessageName")?.ToString() ?? string.Empty;
            var messageVersion = messageMetadata.GetValue("MessageVersion")?.ToString() ?? string.Empty;
            var contextId = messageMetadata.GetValue("ContextId")?.ToString() ?? string.Empty;
            var occurredAt = messageMetadata.GetValue("OccurredAt")?.ToObject<DateTimeOffset>() ?? throw new Exception("No OccurredAt on message.");

            return new MessageMetadata
            {
                MessageName = messageName,
                MessageVersion = messageVersion,
                ContextId = contextId,
                OccurredAt = occurredAt
            };
        }

        public async Task<bool> ProcessMessage(SQSEvent.SQSMessage message)
        {
            var metadata = ValidateAndExtractMetadata(message, out var messageName);

            Log.Information("Message type is {MessageName}", messageName);
            var messageHandler = ValidateAndGetMessageHandler(messageName);

            if (messageHandler is null)
            {
                Log.Information("Message with name {MessageName} is not relevant. Ignoring.", messageName);
                return false;
            }

            var trackingDecision = await _trackerRepository.AttemptToTrackMessageAsync(metadata.MessageName, metadata.MessageVersion, metadata.ContextId, metadata.OccurredAt);

            switch (trackingDecision)
            {
                case TrackingDecision.ProcessMessage:
                    Log.Information("Message needs to be processed!");
                    Log.Information("Processing...");
                    var payload = ValidateAndExtractPayload(message);
                    await messageHandler.HandleAsync(payload);
                    break;
                case TrackingDecision.NewerMessageAlreadyReceived:
                    Log.Information("We've already processed this message! Ignoring...");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return true;
        }
    }
}
