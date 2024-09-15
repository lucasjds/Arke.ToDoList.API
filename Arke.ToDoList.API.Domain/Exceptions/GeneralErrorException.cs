using System.Diagnostics.CodeAnalysis;

namespace Arke.ToDoList.API.Domain.Exceptions;

[ExcludeFromCodeCoverage]
public class GeneralErrorException : Exception
{
    public GeneralErrorException(string property, string messageError)
        : base(EnrichMessage(property, messageError))
    {
    }

    private static string EnrichMessage(string property, string messageError)
    {
        return $"{property}: {messageError}";
    }
}
