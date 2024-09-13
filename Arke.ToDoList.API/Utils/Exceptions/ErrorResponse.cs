namespace Arke.ToDoList.API.Utils.Exceptions;

public class ErrorResponse
{
    public ErrorResponse(string msg, int status)
    {
        Status = status;
        Msg = msg;
        Date = DateTime.Now;
    }

    public int Status { get; private set; }

    public string Msg { get; private set; }

    public DateTime Date { get; private set; }

}
