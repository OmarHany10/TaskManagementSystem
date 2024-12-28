namespace TaskManagementSystem.Core.interfaces
{
    public interface ITaskRepository: IBaseRepository<Models.Task>
    {
        public  List<Models.Task> GetTasksByUserId(string id);

        public List<Models.Task> GetTasksByProjectId(int id);
        public List<Models.Task> GetAllOverdue();
        public List<Models.Task> GetAllUpcoming();
        public List<Models.Task> GetAllFinished();
        public List<Models.Task> GetAllUserOverdue(string userId);
        public List<Models.Task> GetAllUserUpcoming(string userId);
        public List<Models.Task> GetAllUserFinished(string userId);

    }
}
