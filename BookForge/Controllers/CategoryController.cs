using BookForge.DataAccess.Data;
using BookForge.DataAccess.Repository.IRepository;
using BookForge.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookForge.Controllers
{
    public class CategoryController : Controller 
    {
        /** private readonly ApplicationDbContext _db;
               public CategoryController(ApplicationDbContext db)
               {
                    _db = db;
                    
                } **/

        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(ICategoryRepository db)
        {
            _categoryRepo = db;
        }
        public IActionResult Index()
        {
            // display all categories from database
            // List<Category> objCategoryList = _db.Categories.ToList();

            List<Category> objCategoryList = _categoryRepo.GetAll().ToList();
            return View(objCategoryList);
        }

      public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create( Category obj)
        {
            // server side validation
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
               // _db.Categories.Add(obj);

                _categoryRepo.Add(obj);

                // to create the category in database
                //_db.SaveChanges();

                _categoryRepo.Save();

                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        // pass the id to the edit method to find the category in database
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            // find the category in database by id only primary key
          //  Category categoryfromDb = _db.Categories.Find(id);
            // search by any field top 1 record
            //Category categoryfromDb2 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            // find by any field top 1 record if not found return null ( filltering)
            //Category categoryfromDb3 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

            Category? categoryfromDb = _categoryRepo.Get(u => u.Id == id);


            if (categoryfromDb == null)
            {
                return NotFound();
            }
            // if its found pass it to the view
            return View(categoryfromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            
            if (ModelState.IsValid)
            {
               // _db.Categories.Update(obj);

                _categoryRepo.Update(obj);

               // _db.SaveChanges();

                _categoryRepo.Save();

                TempData["success"] = "Category updated successfully";

                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

           // Category categoryfromDb = _db.Categories.Find(id);
           
            Category? categoryfromDb = _categoryRepo.Get(u => u.Id == id);

            if (categoryfromDb == null)
            {
                return NotFound();
            }

            return View(categoryfromDb);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            // find the specidif id 
           // Category? obj = _db.Categories.Find(id);

            Category? obj = _categoryRepo.Get(u => u.Id == id);

            if(obj==null)
            {
                
                return NotFound();
            }
           
               // _db.Categories.Remove(obj);

            _categoryRepo.Remove(obj);

               // _db.SaveChanges();

            _categoryRepo.Save();

            // alert message to user after delete the category
            TempData["success"] = "Category deleted successfully";
                return RedirectToAction("Index");
            
            
        }
    }
}
