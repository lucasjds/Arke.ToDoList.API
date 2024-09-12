namespace Arke.ToDoList.API.Utils.Exceptions;

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
