using System.Text.Json.Serialization;

namespace ExampleBlazorApp.Client.Models
{
    public class ApiErrorDetails
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string TraceId { get; set; }
        public Dictionary<string, IEnumerable<string>> Errors { get; set; }
    }
}