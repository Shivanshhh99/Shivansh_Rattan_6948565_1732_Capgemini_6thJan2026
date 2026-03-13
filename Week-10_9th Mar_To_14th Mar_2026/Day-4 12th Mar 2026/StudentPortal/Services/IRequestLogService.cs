using StudentPortal.Models;

namespace StudentPortal.Services
{
    public interface IRequestLogService
    {
        void AddLog(RequestLog log);

        List<RequestLog> GetLogs();
    }
}