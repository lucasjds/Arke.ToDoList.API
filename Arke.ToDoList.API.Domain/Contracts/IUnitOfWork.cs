namespace Arke.ToDoList.API.Domain.Contracts;

public interface IUnitOfWork
{
    Task<int> CommitAsync();
}
