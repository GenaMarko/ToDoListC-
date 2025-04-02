using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class CategoryModel
    {
        [Key] public string categoryId { get; set; } = string.Empty;

        public string categoryName { get; set; } = string.Empty;


    }
}
