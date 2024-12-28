using TaskManagementSystem.Core.DTOs.ActivityLogDTOs;

namespace TaskManagementSystem.Core.Services.interfaces
{
    public interface IActivityService
    {
        List<ActivityLogDTO> GetAllActivity();
        List<ActivityLogDTO> GetAllUserActivity(string userId);
        ActivityLogDTO GetActivityById(int id);
        Task<string> AddActivity(ActivityLogDTO activityDTO);
        Task<string> UpdateActivity(int id, ActivityLogDTO activityDTO);
        int DeleteActivity(int id);
    }
}
