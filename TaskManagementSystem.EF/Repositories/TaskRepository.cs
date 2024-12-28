using TaskManagementSystem.Core.interfaces;
using TaskManagementSystem.Core.Models;

namespace TaskManagementSystem.EF.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext context;

        public TaskRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Core.Models.Task Add(Core.Models.Task entity)
        {
            context.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            var task = GetByID(id);
            context.Tasks.Remove(task);
        }

        public IEnumerable<Core.Models.Task> GetAll()
        {
            return context.Tasks.ToList();
        }

        public List<Core.Models.Task> GetAllFinished()
        {
            return context.Tasks.Where(t => t.CompleteDate != null).ToList();
        }

        public List<Core.Models.Task> GetAllOverdue()
        {
            return context.Tasks.Where(t => t.DueDate < DateTime.Now && t.CompleteDate == null).ToList();
        }

        public List<Core.Models.Task> GetAllUpcoming()
        {
            return context.Tasks.Where(t => t.DueDate > DateTime.Now && t.CompleteDate == null).ToList();
        }

        public List<Core.Models.Task> GetAllUserFinished(string userId)
        {
            return context.Tasks.Where(t => t.CompleteDate != null && t.UserID == userId).ToList();
        }

        public List<Core.Models.Task> GetAllUserOverdue(string userId)
        {
            return context.Tasks.Where(t => t.DueDate < DateTime.Now && t.CompleteDate == null && t.UserID == userId).ToList();
        }

        public List<Core.Models.Task> GetAllUserUpcoming(string userId)
        {
            return context.Tasks.Where(t => t.CompleteDate == null && t.DueDate > DateTime.Now && t.UserID == userId).ToList();
        }

        public Core.Models.Task GetByID(int id)
        {
            return context.Tasks.FirstOrDefault(t => t.Id == id);
        }

        public List<Core.Models.Task> GetTasksByProjectId(int id)
        {
            var task = context.Tasks.Where(t => t.ProjectID == id).ToList();
            return task;
        }

        public List<Core.Models.Task> GetTasksByUserId(string id)
        {
            var task = context.Tasks.Where(t => t.UserID == id).ToList();
            return task;
        }

        public Core.Models.Task Update(Core.Models.Task entity)
        {
            context.Update(entity);
            return entity;
        }
    }
}
