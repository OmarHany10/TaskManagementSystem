using TaskManagementSystem.Core.interfaces;
using TaskManagementSystem.Core.Models;

namespace TaskManagementSystem.EF.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext context;

        public CommentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Comment Add(Comment entity)
        {
            context.Comments.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            var comment = GetByID(id);
            context.Comments.Remove(comment);
        }

        public IEnumerable<Comment> GetAll()
        {
            return context.Comments;
        }

        public IEnumerable<Comment> GetAllTaskComments(int taskId)
        {
            return context.Comments.Where(c => c.TaskID == taskId).ToList();
        }

        public IEnumerable<Comment> GetAllUserComments(string userId)
        {
            return context.Comments.Where(c => c.UserID == userId);
        }

        public Comment GetByID(int id)
        {
            return context.Comments.FirstOrDefault(c => c.Id == id);
        }

        public Comment Update(Comment entity)
        {
            context.Update(entity);
            return entity;
        }
    }
}
