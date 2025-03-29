namespace ToDoList.Models
{
    public class FilterModel
    {
        public FilterModel(string filterString) 
        {
            filterString = filterString ?? "all-all-all";
            string[] filters = filterString.Split('-');
            categoryId = filters[0];
            due = filters[1];
            statusId = filters[2];
        }

        public string filterString { get; }
        public string categoryId { get; }
        public string due { get; }
        public string statusId { get; }

        public bool hasCategory => categoryId.ToLower() != "all";
        public bool hasDue => due.ToLower() != "all";
        public bool hasStatus => statusId.ToLower() != "all";

        public static Dictionary<string,string> dueFilterValues => 
            new Dictionary<string, string> 
            {
                {"future","Future" },
                {"past","Past" },
                {"today","Today" }
            };

        public bool isPast => due.ToLower() == "past";
        public bool isFuture => due.ToLower() == "future";
        public bool isToday => due.ToLower() == "today";
    }
}
