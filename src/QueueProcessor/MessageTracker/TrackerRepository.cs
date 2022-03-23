using Dapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Serilog;
using System;
using System.Data;
using System.Threading.Tasks;

namespace QueueProcessor.MessageTracker
{
    public class TrackerRepository : ITrackerRepository
    {
        private readonly DbContext _context;

        public TrackerRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<TrackingDecision> AttemptToTrackMessageAsync(
            string messageName,
            string messageVersion,
            string contextId,
            DateTimeOffset occurredAt)
        {
            const string attemptToTrackMessageSql = @"
            INSERT INTO message_tracker 
                (
                 message_name, message_version, context_id, occurred_at
                )
            VALUES
                (
                @p0, 
                @p1, 
                @p2,
                @p3
                )
            ON CONFLICT (message_name, message_version, context_id)
            DO UPDATE 
                SET     occurred_at = @p3
                WHERE   message_tracker.occurred_at < @p3";

            TrackingDecision outcome;

            try
            {
                var rowsAffected = await _context.Database.ExecuteSqlRawAsync(attemptToTrackMessageSql, messageName, messageVersion, contextId, occurredAt);

                outcome = rowsAffected == 0
                    ? TrackingDecision.NewerMessageAlreadyReceived
                    : TrackingDecision.ProcessMessage;
            }
            catch (NpgsqlException)
            {
                Log.Information("An error occurred updating message tracker table. Adding to DLQ.");
                throw;
            }

            return outcome;
        }
    }
}
