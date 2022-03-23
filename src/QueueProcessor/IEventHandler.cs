using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QueueProcessor
{
    public interface IEventHandler
    {
        Task<bool> HandleAsync(JObject messagePayload);
    }
}
