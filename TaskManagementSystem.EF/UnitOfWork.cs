using TaskManagementSystem.Core;
using TaskManagementSystem.Core.interfaces;
using TaskManagementSystem.EF.Repositories;

namespace TaskManagementSystem.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            Users = new UserRepository(context);
            Projects = new ProjectRepository(context);
            Tasks = new TaskRepository(context);
            Comments = new CommentRepository(context);
            ActivityLogs = new ActivityLogRepository(context);
        }
        public IUserRepository Users { get; private set; }

        public IProjectRepository Projects { get; private set; }

        public ITaskRepository Tasks { get; private set; }
        public ICommentRepository Comments { get; private set; }
        public IActivityLogRepository ActivityLogs { get; private set; }


        public void Dispose()
        {
            context.Dispose();
        }

        public int save()
        {
            return context.SaveChanges();
        }
    }
}
