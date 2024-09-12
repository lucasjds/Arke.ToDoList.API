namespace Arke.ToDoList.API.DataAccess.Repositories.Interfaces;

public interface IBaseRepository<TEntity>
   where TEntity : class
{
    Task<IEnumerable<TEntity>> FindAll();
    Task<TEntity> FindById(Guid id);
    Task<TEntity> Save(TEntity entity);
    void Delete(TEntity entity);
}
