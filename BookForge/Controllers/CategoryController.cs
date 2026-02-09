using BookForge.Data;
using BookForge.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookForge.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
            
        }
        public IActionResult Index()
        {
            // display all categories from database
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

      public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create( Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            { 
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            // not under any specific field validation
            if(obj.Name== "test")
            {
                ModelState.AddModelError("", "test is an invalid value" );
            }
            // if the obj valid
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);

                // to create the category in database
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
