using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QueueProcessor
{
    public class PaymentOrderHandler : IEventHandler
    {
        private readonly DbContext _context;
        public PaymentOrderHandler(DbContext context)
        {
            _context = context;
        }
        public async Task<bool> HandleAsync(JObject messagePayload)
        {
            var someProperty = messagePayload.GetValue("someProperty")?.ToString() ?? throw new Exception("Failed to process Payment Order message. Could not get someProperty value from message payload.");

            Log.Information("Getting payment order: {someProperty}", someProperty);

            // Call CkoBankSimulator
            // Update Payment
            return await Task.FromResult(true);
        }
    }
}
