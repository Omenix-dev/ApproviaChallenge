using Newtonsoft.Json;

namespace ApproviaChallenge.TaskManager.Core.Models
{
    public class ErrorHandling
    {
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
