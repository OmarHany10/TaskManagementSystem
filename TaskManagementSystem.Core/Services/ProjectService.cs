using TaskManagementSystem.Core.DTOs.ProjectDTOs;
using TaskManagementSystem.Core.Models;
using TaskManagementSystem.Core.Services.interfaces;

namespace TaskManagementSystem.Core.Services
{
    public class ProjectService : IProjectServices
    {
        private readonly IUnitOfWork unitOfWork;

        public ProjectService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<string> Add(ProjectDTO projectDTO)
        {
            var project = new Project
            {
                Name = projectDTO.Name,
                StartDate = DateTime.Now,
                EndDate = projectDTO.EndDate,
                Description = projectDTO.Description,
                CreatedByID = projectDTO.CreatedBy,
            };
            var user = await unitOfWork.Users.GetByID(projectDTO.CreatedBy);
            if (user == null)
                return "User Not Found";

            project.CreatedBy = user;
            unitOfWork.Projects.Add(project);
            unitOfWork.save();
            return null;    
        }

        public void AssignToTask(int projectId, Models.Task task)
        {
            var project = unitOfWork.Projects.GetByID(projectId);
            if(project.Tasks == null)
                project.Tasks = new List<Models.Task>();

            project.Tasks.Add(task);
            unitOfWork.save();
        }

        public int Delete(int id)
        {
            var tasks = unitOfWork.Tasks.GetTasksByProjectId(id);
            if(tasks == null)
                return 2;
            unitOfWork.Projects.Delete(id);
            return unitOfWork.save();
        }

        public IEnumerable<ProjectDTO> GetAll()
        {
            var projects = unitOfWork.Projects.GetAll();
            List<ProjectDTO> result = new List<ProjectDTO>();
            foreach (var project in projects)
            {
                var projectDTO = new ProjectDTO
                {
                    Name = project.Name,
                    Description = project.Description,
                    EndDate = project.EndDate,
                    CreatedBy = project.CreatedByID,
                };
                result.Add(projectDTO);
            }
            return result;
        }

        public async Task<List<ApplicationUser>> GetAllUsers(int projectId)
        {
            var tasks = unitOfWork.Tasks.GetTasksByProjectId(projectId);
            var users = new List<ApplicationUser>();
            foreach (var task in tasks)
            {
                var user = await unitOfWork.Users.GetByID(task.UserID);
                user.Tasks = null;
                if(users.Contains(user))
                    continue;
                users.Add(user);
            }
            return users;
        }

        public ProjectDTO GetByID(int id)
        {
            var project = unitOfWork.Projects.GetByID(id);
            if (project == null)
                return null;
            ProjectDTO projectDTO = new ProjectDTO
            {
                Name = project.Name,
                Description = project.Description,
                EndDate = project.EndDate,
                CreatedBy = project.CreatedByID,
            };
            return projectDTO;
        }

        public async Task<string> Update(int id, ProjectDTO projectDTO)
        {
            var project = unitOfWork.Projects.GetByID(id);
            if (project == null)
                return $"No Project have this ID: {id}";

            var user = await unitOfWork.Users.GetByID(projectDTO.CreatedBy);
            if (user == null)
                return $"No User have this ID: {projectDTO.CreatedBy}";

            project.Name = projectDTO.Name;
            project.Description = projectDTO.Description;
            project.EndDate = projectDTO.EndDate;
            project.CreatedByID = projectDTO.CreatedBy;
            project.CreatedBy = user;
            unitOfWork.Projects.Update(project);
            unitOfWork.save();
            return null;
        }

        
    }
}
