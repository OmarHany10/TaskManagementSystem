using TaskManagementSystem.Core.DTOs.CommentDTOs;
using TaskManagementSystem.Core.Models;
using TaskManagementSystem.Core.Services.interfaces;

namespace TaskManagementSystem.Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<string> AddComment(CommentDTO commentDTO)
        {
            var user = await unitOfWork.Users.GetByID(commentDTO.UserID);
            if (user == null)
                return "User Not Found";
            var task = unitOfWork.Tasks.GetByID(commentDTO.TaskID);
            if (task == null)
                return "Task Not Found";
            Comment comment = new Comment
            {
                CreatedAt = DateTime.Now,
                TaskID = commentDTO.TaskID,
                UserID = commentDTO.UserID,
                Task = task,
                User = user,
                Text = commentDTO.Text,
            };
            unitOfWork.Comments.Add(comment);
            unitOfWork.save();
            return null;
        }

        public int DeleteComment(int id)
        {
            var comment = unitOfWork.Comments.GetByID(id);
            if(comment == null) return 0;
            unitOfWork.Comments.Delete(id);
            return 1;
        }

        public List<CommentDTO> GetAllComments()
        {
            var comments = unitOfWork.Comments.GetAll();
            var result = new List<CommentDTO>();
            foreach(var comment in comments)
            {
                var commentDTO = new CommentDTO
                {
                    TaskID = comment.TaskID,
                    Text = comment.Text,
                    UserID = comment.UserID,
                };
                result.Add(commentDTO);
            }
            return result;
        }

        public CommentDTO GetCommentById(int id)
        {
            var comment = unitOfWork.Comments.GetByID(id);
            if (comment == null)
                return null;
            var result = new CommentDTO();
            result.Text = comment.Text;
            result.UserID = comment.UserID;
            result.TaskID = comment.TaskID;
            return result;
        }

        public List<CommentDTO> GetTaskComments(int taskId)
        {
            var comments = unitOfWork.Comments.GetAllTaskComments(taskId);
            var result = new List<CommentDTO>();
            foreach (var comment in comments)
            {
                var commentDTO = new CommentDTO
                {
                    TaskID = comment.TaskID,
                    Text = comment.Text,
                    UserID = comment.UserID,
                };
                result.Add(commentDTO);
            }
            return result;
        }

        public List<CommentDTO> GetUserComments(string userID)
        {
            var comments = unitOfWork.Comments.GetAllUserComments(userID);
            var result = new List<CommentDTO>();
            foreach (var comment in comments)
            {
                var commentDTO = new CommentDTO
                {
                    TaskID = comment.TaskID,
                    Text = comment.Text,
                    UserID = comment.UserID,
                };
                result.Add(commentDTO);
            }
            return result;
        }

        public async Task<string> UpdateComment(int id, CommentDTO commentDTO)
        {
            var comment = unitOfWork.Comments.GetByID(id);
            if(comment == null)
                return "Comment Not Found";
            var user = await unitOfWork.Users.GetByID(commentDTO.UserID);
            if (user == null)
                return "User Not Found";
            var task = unitOfWork.Tasks.GetByID(commentDTO.TaskID);
            if (task == null)
                return "Task Not Found";

            comment.TaskID = commentDTO.TaskID;
            comment.UserID = commentDTO.UserID;
            comment.Task = task;
            comment.User = user;
            comment.Text = commentDTO.Text;
            unitOfWork.Comments.Update(comment);
            unitOfWork.save();
            return null;
        }
    }
}
