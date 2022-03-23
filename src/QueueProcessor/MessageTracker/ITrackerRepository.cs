using System;
using System.Data;
using System.Threading.Tasks;

namespace QueueProcessor.MessageTracker
{
    public interface ITrackerRepository
    {
        /// <summary>
        /// Attempt to update the message_tracker table to process a message.
        /// </summary>
        /// <param name="db">The database connection.</param>
        /// <param name="tx">The transaction that we'll be running all processing of the message within.</param>
        /// <param name="messageName">The name of the message.</param>
        /// <param name="messageVersion">The version of the message.</param>
        /// <param name="contextId">The context identifier for the message.</param>
        /// <param name="occurredAt">The timestamp that the message occurred at.</param>
        /// <returns>
        /// <para>TrackingDecision.ProcessMessage: Indicates we should proceed and process the message</para>
        /// <para>TrackingDecision.NewerMessageAlreadyReceived: Indicates that we can discard the message we have just
        /// received, as we have already processed a newer one</para>
        /// <para>TrackingDecision.RetryMessageLater: Indicates
        /// we should return the message to the queue as the system is currently in the middle of processing a
        /// message of the same type.</para>
        /// </returns>
        Task<TrackingDecision> AttemptToTrackMessageAsync(
            string messageName,
            string messageVersion,
            string contextId,
            DateTimeOffset occurredAt);
    }
}
