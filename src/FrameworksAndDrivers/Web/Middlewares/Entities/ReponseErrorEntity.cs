using System;
using System.Text.Json;

namespace FrameworksAndDrivers.Web.Middlewares.Entities
{
    public class ReponseErrorEntity
    {
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Path { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public string RequestId { get; set; }
        public string Method { get; set; }
        public string Stack { get; set; }

        public override string ToString()  => 
            $"[{RequestId}] [{Status}] <{Method}> <{Path}> - {Message}";

        public string ToJson()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });
        }
    }
}