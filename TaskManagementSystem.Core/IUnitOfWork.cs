using TaskManagementSystem.Core.interfaces;
using TaskManagementSystem.Core.Services.interfaces;

namespace TaskManagementSystem.Core
{
    public interface IUnitOfWork: IDisposable
    {
        IUserRepository Users { get; }
        IProjectRepository Projects { get; }
        ITaskRepository Tasks { get; }
        ICommentRepository Comments { get; }
        IActivityLogRepository ActivityLogs { get; }

        int save();
    }
}
