using TaskManagementSystem.Core.interfaces;

namespace TaskManagementSystem.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext context;

        public BaseRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public T Add(T entity)
        {
            context.Set<T>().Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            var entity = GetByID(id);
            context.Set<T>().Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public T GetByID(int id)
        {
            return context.Set<T>().Find(id);
        }

        public T Update(T entity)
        {
            context.Set<T>().Update(entity);
            return entity;
        }
    }
}
