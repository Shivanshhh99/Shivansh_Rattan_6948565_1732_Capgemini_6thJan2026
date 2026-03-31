namespace StudentPortal.Models
{
    public class RequestLog
    {
        public string Url { get; set; }

        public long ExecutionTime { get; set; }

        public DateTime RequestTime { get; set; }
    }
}