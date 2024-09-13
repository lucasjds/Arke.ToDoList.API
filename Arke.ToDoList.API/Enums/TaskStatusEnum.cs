using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Arke.ToDoList.API.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum TaskStatusEnum
{
    ToDo = 1,
    InProgress = 2,
    Done = 3
}
