using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        private ToDoContext context;
        public HomeController(ToDoContext ctx) => context = ctx; 
        public IActionResult Index(string id)
        {
            var filters = new FilterModel(id);   

            ViewBag.Filters = filters;
            ViewBag.Categories = context.category.ToList();
            ViewBag.Statuses = context.status.ToList();
            ViewBag.DueFilters = FilterModel.dueFilterValues;

            IQueryable<ToDoModel> query = context.toDos
                .Include(t => t.category)
                .Include(t => t.status);

            if (filters.hasCategory) 
            {
                query = query.Where(t => t.categoryId == filters.categoryId);
            }
            if (filters.hasStatus)
            {
                query = query.Where(t => t.statusId == filters.statusId);
            }
            if (filters.hasDue) 
            {
                var today = DateTime.Today;

                if (filters.isPast)
                {
                    query = query.Where(t => t.dueDate < today);
                }
            }

            return View();
        }
    }
}
