using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class StatusModel
    {
        [Key] public string statusId { get; set; } = string.Empty;

        public string statusName { get; set; } = string.Empty;
    }
}
