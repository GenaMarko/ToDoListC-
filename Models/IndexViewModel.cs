namespace ToDoList.Models
{
    public class IndexViewModel
    {
        public IEnumerable<ToDoModel> toDoModels { get; set; }
        public PageViewModel pageViewModel { get; set; }
    }
}
