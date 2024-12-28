using TaskManagementSystem.Core.Models;

namespace TaskManagementSystem.Core.interfaces
{
    public interface ICommentRepository: IBaseRepository<Comment>
    {
        IEnumerable<Comment> GetAllTaskComments(int taskId);
        IEnumerable<Comment> GetAllUserComments(string userId);
    }
}
