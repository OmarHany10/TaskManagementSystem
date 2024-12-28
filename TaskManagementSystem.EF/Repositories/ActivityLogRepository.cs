
using TaskManagementSystem.Core.interfaces;
using TaskManagementSystem.Core.Models;

namespace TaskManagementSystem.EF.Repositories
{
    public class ActivityLogRepository : IActivityLogRepository
    {
        private readonly ApplicationDbContext context;

        public ActivityLogRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public ActivityLog Add(ActivityLog entity)
        {
            context.ActivityLogs.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            var activity = GetByID(id);
            context.ActivityLogs.Remove(activity);
        }

        public IEnumerable<ActivityLog> GetAll()
        {
            return context.ActivityLogs.ToList();
        }

        public List<ActivityLog> GetAllUserActivityLog(string Id)
        {
            return context.ActivityLogs.Where(a => a.UserID == Id).ToList();
        }

        public ActivityLog GetByID(int id)
        {
            return context.ActivityLogs.FirstOrDefault(x => x.Id == id);
        }

        public ActivityLog Update(ActivityLog entity)
        {
            context.ActivityLogs.Update(entity);
            return entity;
        }
    }
}
