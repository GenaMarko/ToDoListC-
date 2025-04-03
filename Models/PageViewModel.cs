namespace ToDoList.Models
{
    public class PageViewModel
    {
        public int pageNumbers { get; set; }
        public int totalPages { get; set; }

        public PageViewModel(int count,int _pageNumbers, int pageSize) 
        {
            pageNumbers = _pageNumbers;
            totalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool hasPreviousPage { get { return pageNumbers > 1; } }
        public bool hasNextPage { get { return pageNumbers < totalPages; } }
    }
}
