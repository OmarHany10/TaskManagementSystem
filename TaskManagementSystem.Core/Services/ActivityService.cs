using TaskManagementSystem.Core.DTOs.ActivityLogDTOs;
using TaskManagementSystem.Core.Models;
using TaskManagementSystem.Core.Services.interfaces;

namespace TaskManagementSystem.Core.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IUnitOfWork unitOfWork;

        public ActivityService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<string> AddActivity(ActivityLogDTO activityDTO)
        {
            var user = await unitOfWork.Users.GetByID(activityDTO.UserID);
            if (user == null)
                return "User Not Found";
            var activity = new ActivityLog
            {
                Action = activityDTO.Action,
                User = user,
                UserID = activityDTO.UserID,
                EntityID = activityDTO.EntityID,
                TimeStamp = activityDTO.TimeStamp,
                EntityType = activityDTO.EntityType == "Project" ? EntityType.Project : EntityType.Task
            };
            if(user.ActivityLogs == null)
                user.ActivityLogs = new List<ActivityLog>();
            user.ActivityLogs.Add(activity);
            unitOfWork.ActivityLogs.Add(activity);
            unitOfWork.save();
            return "Completed";
        }

        public int DeleteActivity(int id)
        {
            var activity = unitOfWork.ActivityLogs.GetByID(id);
            if(activity == null)
                return 0;
            unitOfWork.ActivityLogs.Delete(id);
            unitOfWork.save();
            return 1;
        }

        public ActivityLogDTO GetActivityById(int id)
        {
            var activity = unitOfWork.ActivityLogs.GetByID(id);
            ActivityLogDTO activityLog = null;
            if (activity == null)
                return activityLog;

            activityLog = new ActivityLogDTO
            {
                Action = activity.Action,
                UserID = activity.UserID,
                EntityID = activity.EntityID,
                TimeStamp = activity.TimeStamp,
                EntityType = activity.EntityType.ToString()
            };
            return activityLog;
        }

        public List<ActivityLogDTO> GetAllActivity()
        {
            var Activities = unitOfWork.ActivityLogs.GetAll();
            var result = new List<ActivityLogDTO>();
            foreach(var activity in Activities)
            {
                var activityDTO = new ActivityLogDTO
                {
                    Action = activity.Action,
                    UserID = activity.UserID,
                    EntityID = activity.EntityID,
                    TimeStamp = activity.TimeStamp,
                    EntityType = activity.EntityType.ToString()
                };
                result.Add(activityDTO);
            }
            return result;
        }

        public List<ActivityLogDTO> GetAllUserActivity(string userId)
        {
            var Activities = unitOfWork.ActivityLogs.GetAllUserActivityLog(userId);
            var result = new List<ActivityLogDTO>();
            foreach (var activity in Activities)
            {
                var activityDTO = new ActivityLogDTO
                {
                    Action = activity.Action,
                    UserID = activity.UserID,
                    EntityID = activity.EntityID,
                    TimeStamp = activity.TimeStamp,
                    EntityType = activity.EntityType.ToString()
                };
                result.Add(activityDTO);
            }
            return result;
        }

        public async Task<string> UpdateActivity(int id, ActivityLogDTO activityDTO)
        {
            var user = await unitOfWork.Users.GetByID(activityDTO.UserID);
            if (user == null)
                return "User Not Found";
            var activity = unitOfWork.ActivityLogs.GetByID(id);
            activity = new ActivityLog
            {
                Action = activityDTO.Action,
                User = user,
                UserID = activityDTO.UserID,
                EntityID = activityDTO.EntityID,
                TimeStamp = activityDTO.TimeStamp,
                EntityType = activityDTO.EntityType == "Project" ? EntityType.Project : EntityType.Task
            };

            unitOfWork.ActivityLogs.Update(activity);
            unitOfWork.save();
            return "Updated";
        }
    }
}
