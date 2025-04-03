using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ToDoList.Models;
using X.PagedList;
using X.PagedList.Extensions;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        private ToDoContext context;
        public HomeController(ToDoContext ctx) => context = ctx; 
        public IActionResult Index(string id, int? page)
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
                else if (filters.isFuture)
                {
                    query = query.Where(t => t.dueDate > today);
                }
                if (filters.isToday)
                {
                    query = query.Where(t => t.dueDate == today);
                }
            }

            var tasks = query.OrderBy(t => t.dueDate).ToList();

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(tasks.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult Add() 
        {
            ViewBag.Categories = context.category.ToList();
            ViewBag.Statuses = context.status.ToList();

            var task = new ToDoModel { statusId = "open" };

            return View(task);
        }

        [HttpPost]
        public IActionResult Add(ToDoModel task) 
        {
            if (ModelState.IsValid)
            {
                context.toDos.Add(task);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            else 
            {
                ViewBag.Categories = context.category.ToList();
                ViewBag.Statuses = context.status.ToList();

                return View(task);
            }
        }

        [HttpPost]
        public IActionResult Filter(string[] filter)
        {
            string id = string.Join("-", filter);

            return RedirectToAction("Index",new {ID = id});
        }

        [HttpPost]
        public IActionResult MarkComplete([FromRoute] string id, ToDoModel selected) 
        { 
            selected = context.toDos.Find(selected.id);

            if (selected != null)
            {
                selected.statusId = "closed";
                context.SaveChanges();
            }

            return RedirectToAction("Index", new {ID = id});
        }

        [HttpPost]
        public IActionResult DeleteComplete(string id) 
        {
            var toDelete = context.toDos.Where(t => t.statusId == "closed").ToList();

            foreach (var task in toDelete)
            {
                context.toDos.Remove(task);
            }

            context.SaveChanges();

            return RedirectToAction("Index", new {ID = id});
        }
    }
}
