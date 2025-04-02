using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class ToDoModel
    {
        [Key] public int id { get; set; }

        [Required(ErrorMessage = "Please enter a description.")]
        public string description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a due date.")]
        public DateTime? dueDate { get; set; }

        [Required(ErrorMessage = "Please select a category.")]
        public string categoryId { get; set; } = string.Empty;
        [ValidateNever]
        public CategoryModel category { get; set; } = null!;

        [Required(ErrorMessage = "Please select a status.")]
        public string statusId { get; set; } = string.Empty;
        [ValidateNever]
        public StatusModel status { get; set; } = null!;

        public bool overDue => statusId == "open" && dueDate < DateTime.Today;
    }
}
