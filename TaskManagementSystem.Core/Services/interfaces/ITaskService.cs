using TaskManagementSystem.Core.DTOs;

namespace TaskManagementSystem.Core.Services.interfaces
{
    public interface ITaskService
    {
        public IEnumerable<TaskDTO> GetAll();
        public TaskDTO GetById(int id);
        public Task<string> Add(TaskDTO taskDTO);
        public Task<string> Update(int id, TaskDTO taskDTO);
        public int Delete(int id);
        public List<TaskDTO> GetTasksAssignToUser(string userId);
        public List<TaskDTO> GetTasksByProject(int projectId);
        public Task<string> AssignTaskToUser(string userId, int taskId);
        public IList<TaskDTO> GetAllOverdue();
        public IList<TaskDTO> GetAllUpcoming();
        public IList<TaskDTO> GetAllFinished();
        public List<TaskDTO> GetAllUserOverdue(string userId);
        public List<TaskDTO> GetAllUserUpcoming(string userId);
        public List<TaskDTO> GetAllUserFinished(string userId);

        public string FinishTheTask(int taskId);

    }
}
