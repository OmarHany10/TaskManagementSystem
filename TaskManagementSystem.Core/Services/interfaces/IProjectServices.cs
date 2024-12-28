using TaskManagementSystem.Core.DTOs.ProjectDTOs;

namespace TaskManagementSystem.Core.Services.interfaces
{
    public interface IProjectServices
    {
        public IEnumerable<ProjectDTO> GetAll();
        public ProjectDTO GetByID(int id);
        public Task<string> Add(ProjectDTO projectDTO);
        public Task<string> Update(int id, ProjectDTO projectDTO);
        public int Delete(int id);
        public Task<List<ApplicationUser>> GetAllUsers(int projectId);
        public void AssignToTask(int projectId ,Models.Task task);    

    }
}
