using StudentPortal.Models;

namespace StudentPortal.Services
{
    public class RequestLogService : IRequestLogService
    {
        private readonly List<RequestLog> _logs = new List<RequestLog>();

        public void AddLog(RequestLog log)
        {
            _logs.Add(log);
        }

        public List<RequestLog> GetLogs()
        {
            return _logs;
        }
    }
}