namespace Arke.ToDoList.API.DataAccess.UnitOfWork;

public interface IUnitOfWork
{
    Task<int> CommitAsync();
}
