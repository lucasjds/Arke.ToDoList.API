using System.Diagnostics.CodeAnalysis;

namespace Arke.ToDoList.API.Utils.Exceptions;

[ExcludeFromCodeCoverage]
public class NotFoundException : Exception
{
    public NotFoundException(string entity, Guid id)
        : base(EnrichMessage(entity, id))
    {
    }

    private static string EnrichMessage(string entity, Guid id)
    {
        return $"{entity} with Id: '{id}' does not exist.";
    }
}
