using TaskManagementSystem.Core.DTOs;
using TaskManagementSystem.Core.DTOs.ActivityLogDTOs;
using TaskManagementSystem.Core.Models;
using TaskManagementSystem.Core.Services.interfaces;

namespace TaskManagementSystem.Core.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IProjectServices projectServices;
        private readonly IActivityService activityService;

        public TaskService(IUnitOfWork unitOfWork, IProjectServices projectServices, IActivityService activityService)
        {
            this.unitOfWork = unitOfWork;
            this.projectServices = projectServices;
            this.activityService = activityService;
        }
        public async Task<string> Add(TaskDTO taskDTO)
        {
            var user = await unitOfWork.Users.GetByID(taskDTO.UserID);
            if (user == null)
                return "User Not Found";

            var project = unitOfWork.Projects.GetByID(taskDTO.ProjectID);
            if (project == null)
                return "Project Not Found";

            if (taskDTO.StartDate > taskDTO.DueDate)
                return "Error end date must be after start date";

            TaskPriority taskPriority = new TaskPriority();
            if (taskDTO.Priority == TaskPriority.Medium.ToString())
                taskPriority = TaskPriority.Medium;
            else if (taskDTO.Priority == TaskPriority.High.ToString())
                taskPriority = TaskPriority.High;
            else
                taskPriority = TaskPriority.Low;

            Models.Task task = new Models.Task
            {
                Name = taskDTO.Name,
                Description = taskDTO.Description,
                StartDate = taskDTO.StartDate,
                DueDate = taskDTO.DueDate,
                ProjectID = taskDTO.ProjectID,
                Project = project,
                User = user,
                UserID = taskDTO.UserID,
                Status = Models.TaskStatus.ToDo,
                Priority = taskPriority,
            };
            unitOfWork.Tasks.Add(task);
            unitOfWork.save();
            //if(user.Tasks == null)
            //    user.Tasks = new List<Models.Task>();
            //user.Tasks.Add(task);
            //unitOfWork.save();
            //projectServices.AssignToTask(task.ProjectID, task);
            var activity = new ActivityLogDTO
            {
                Action = $"Task Assigned To User {user.UserName}",
                EntityType = EntityType.Task.ToString(),
                EntityID = task.Id,
                TimeStamp = DateTime.Now,
                UserID = user.Id,
            };
            await activityService.AddActivity(activity);
            unitOfWork.save();
            return null;
        }

        public int Delete(int id)
        {
            unitOfWork.Tasks.Delete(id);
            return unitOfWork.save();
        }

        public IEnumerable<TaskDTO> GetAll()
        {
            var tasks = unitOfWork.Tasks.GetAll();
            var result = new List<TaskDTO>();
            foreach (var task in tasks)
            {
                TaskDTO taskDTO = new TaskDTO
                {
                    Name = task.Name,
                    Description = task.Description,
                    StartDate = task.StartDate,
                    DueDate = task.DueDate,
                    ProjectID = task.ProjectID,
                    CompleteDate = task.CompleteDate,
                    Priority = task.Priority.ToString(),
                    UserID = task.UserID
                };
                result.Add(taskDTO);
            }
            return result;
        }

        public TaskDTO GetById(int id)
        {
            var task = unitOfWork.Tasks.GetByID(id);
            if (task == null)
                return null;
            TaskDTO taskDTO = new TaskDTO
            {
                Name = task.Name,
                Description = task.Description,
                StartDate = task.StartDate,
                DueDate = task.DueDate,
                ProjectID = task.ProjectID,
                CompleteDate = task.CompleteDate,
                Priority = task.Priority.ToString(),
                UserID = task.UserID
            };
            return taskDTO;
        }

        public List<TaskDTO> GetTasksAssignToUser(string userId)
        {
            var tasks = unitOfWork.Tasks.GetTasksByUserId(userId);
            var result = new List<TaskDTO>();
            foreach (var task in tasks)
            {
                TaskDTO taskDTO = new TaskDTO
                {
                    Name = task.Name,
                    Description = task.Description,
                    StartDate = task.StartDate,
                    DueDate = task.DueDate,
                    ProjectID = task.ProjectID,
                    CompleteDate = task.CompleteDate,
                    Priority = task.Priority.ToString(),
                    UserID = task.UserID
                };
                result.Add(taskDTO);
            }
            return result;

        }

        public List<TaskDTO> GetTasksByProject(int projectId)
        {
            var tasks = unitOfWork.Tasks.GetTasksByProjectId(projectId);
            var result = new List<TaskDTO>();
            foreach (var task in tasks)
            {
                TaskDTO taskDTO = new TaskDTO
                {
                    Name = task.Name,
                    Description = task.Description,
                    StartDate = task.StartDate,
                    DueDate = task.DueDate,
                    ProjectID = task.ProjectID,
                    CompleteDate = task.CompleteDate,
                    Priority = task.Priority.ToString(),
                    UserID = task.UserID
                };
                result.Add(taskDTO);
            }
            return result;
        }

        public async Task<string> Update(int id, TaskDTO taskDTO)
        {
            var oldTask = unitOfWork.Tasks.GetByID(id);

            if (oldTask == null)
                return "Task Not Found";

            if (taskDTO.UserID == null)
                return "User Not Found";

            var user = await unitOfWork.Users.GetByID(taskDTO.UserID);
            if (user == null)
                return "User Not Found";

            var project = unitOfWork.Projects.GetByID(taskDTO.ProjectID);
            if (project == null)
                return "Project Not Found";

            if (taskDTO.StartDate > taskDTO.DueDate)
                return "Error end date must be after start date";

            TaskPriority taskPriority = new TaskPriority();
            if (taskDTO.Priority == TaskPriority.Medium.ToString())
                taskPriority = TaskPriority.Medium;
            else if (taskDTO.Priority == TaskPriority.High.ToString())
                taskPriority = TaskPriority.High;
            else
                taskPriority = TaskPriority.Low;

            oldTask.Name = taskDTO.Name;
            oldTask.Description = taskDTO.Description;
            oldTask.Priority = taskPriority;
            oldTask.StartDate = taskDTO.StartDate;
            oldTask.DueDate = taskDTO.DueDate;
            oldTask.ProjectID = taskDTO.ProjectID;
            oldTask.Project = project;
            oldTask.UserID = taskDTO.UserID;
            oldTask.User = user;
            unitOfWork.Tasks.Update(oldTask);
            unitOfWork.save();
            return null;

        }

        public async Task<string> AssignTaskToUser(string userId, int taskId)
        {
            var user = await unitOfWork.Users.GetByID(userId);
            if (user == null)
                return "User Not Found";
            var task = unitOfWork.Tasks.GetByID(taskId);
            if (task == null)
                return "TaskNotFound";
            if (user.Tasks == null)
                user.Tasks = new List<Models.Task>();
            user.Tasks.Add(task);
            task.User = user;
            task.UserID = userId;
            unitOfWork.save();
            return null;
        }

        public IList<TaskDTO> GetAllOverdue()
        {
            var tasks = unitOfWork.Tasks.GetAllOverdue();
            var result = new List<TaskDTO>();
            foreach (var task in tasks)
            {
                TaskDTO taskDTO = new TaskDTO
                {
                    Name = task.Name,
                    Description = task.Description,
                    StartDate = task.StartDate,
                    DueDate = task.DueDate,
                    ProjectID = task.ProjectID,
                    CompleteDate = task.CompleteDate,
                    Priority = task.Priority.ToString(),
                    UserID = task.UserID
                };
                result.Add(taskDTO);
            }
            return result;
        }

        public IList<TaskDTO> GetAllUpcoming()
        {
            var tasks = unitOfWork.Tasks.GetAllUpcoming();
            var result = new List<TaskDTO>();
            foreach (var task in tasks)
            {
                TaskDTO taskDTO = new TaskDTO
                {
                    Name = task.Name,
                    Description = task.Description,
                    StartDate = task.StartDate,
                    DueDate = task.DueDate,
                    ProjectID = task.ProjectID,
                    CompleteDate = task.CompleteDate,
                    Priority = task.Priority.ToString(),
                    UserID = task.UserID
                };
                result.Add(taskDTO);
            }
            return result;
        }

        public IList<TaskDTO> GetAllFinished()
        {
            var tasks = unitOfWork.Tasks.GetAllFinished();
            var result = new List<TaskDTO>();
            foreach (var task in tasks)
            {
                TaskDTO taskDTO = new TaskDTO
                {
                    Name = task.Name,
                    Description = task.Description,
                    StartDate = task.StartDate,
                    DueDate = task.DueDate,
                    ProjectID = task.ProjectID,
                    CompleteDate = task.CompleteDate,
                    Priority = task.Priority.ToString(),
                    UserID = task.UserID
                };
                result.Add(taskDTO);
            }
            return result;
        }

        public List<TaskDTO> GetAllUserOverdue(string userId)
        {
            var tasks = unitOfWork.Tasks.GetAllUserOverdue(userId);
            var result = new List<TaskDTO>();
            foreach (var task in tasks)
            {
                TaskDTO taskDTO = new TaskDTO
                {
                    Name = task.Name,
                    Description = task.Description,
                    StartDate = task.StartDate,
                    DueDate = task.DueDate,
                    ProjectID = task.ProjectID,
                    CompleteDate = task.CompleteDate,
                    Priority = task.Priority.ToString(),
                    UserID = task.UserID
                };
                result.Add(taskDTO);
            }
            return result;
        }

        public List<TaskDTO> GetAllUserUpcoming(string userId)
        {
            var tasks = unitOfWork.Tasks.GetAllUserUpcoming(userId);
            var result = new List<TaskDTO>();
            foreach (var task in tasks)
            {
                TaskDTO taskDTO = new TaskDTO
                {
                    Name = task.Name,
                    Description = task.Description,
                    StartDate = task.StartDate,
                    DueDate = task.DueDate,
                    ProjectID = task.ProjectID,
                    CompleteDate = task.CompleteDate,
                    Priority = task.Priority.ToString(),
                    UserID = task.UserID
                };
                result.Add(taskDTO);
            }
            return result;
        }

        public List<TaskDTO> GetAllUserFinished(string userId)
        {
            var tasks = unitOfWork.Tasks.GetAllUserFinished(userId);
            var result = new List<TaskDTO>();
            foreach (var task in tasks)
            {
                TaskDTO taskDTO = new TaskDTO
                {
                    Name = task.Name,
                    Description = task.Description,
                    StartDate = task.StartDate,
                    DueDate = task.DueDate,
                    ProjectID = task.ProjectID,
                    CompleteDate = task.CompleteDate,
                    Priority = task.Priority.ToString(),
                    UserID = task.UserID
                };
                result.Add(taskDTO);
            }
            return result;
        }

        public string FinishTheTask(int taskId)
        {
            var task = unitOfWork.Tasks.GetByID(taskId);
            if (task == null)
                return "Task Not Found";
            if (task.CompleteDate != null)
                return "Task Already Finished";
            task.CompleteDate = DateTime.Now;
            unitOfWork.Tasks.Update(task);
            unitOfWork.save();
            return $"{task.Name} Finished";
        }
    }
}
