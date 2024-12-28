using TaskManagementSystem.Core.Models;

namespace TaskManagementSystem.Core.interfaces
{
    public interface IActivityLogRepository: IBaseRepository<ActivityLog>
    {
        List<ActivityLog> GetAllUserActivityLog(string Id);
    }
}
