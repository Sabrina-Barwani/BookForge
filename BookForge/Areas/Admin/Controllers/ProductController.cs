using BookForge.DataAccess.Repository.IRepository;
using BookForge.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookForge.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            // get all products

            List<Product> objProducts = _unitOfWork.Product.GetAll().ToList();
            return View(objProducts);
        }

        // create 

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product o)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(o);
                _unitOfWork.save();

                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");

            }
            return View();

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();

            }
            var productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            if (productFromDb == null)
            {
                return NotFound();

            }
            return View(productFromDb);

        }

        [HttpPost]
        public IActionResult Edit(Product o)
        {
            if (o == null)
            {
                return NotFound();

            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(o);
                _unitOfWork.save();

                TempData["success"] = "Product updated successfully";
                return RedirectToAction("Index");

            }
            return View();
        }

        public IActionResult Delete(int? id)
        {// view what u want to delete only for returning the specific info on delete view
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // get all the info based on the id
            var productFromDb = _unitOfWork.Product.Get(u => u.Id == id);

            // if its not found return not found

            if (productFromDb == null)
            {
                return NotFound();
            }

            return View(productFromDb);



        }

        [HttpPost, ActionName("Delete")]

        public IActionResult DeletePOST(int? id)
        {
            // the actual delete process
            // get the id
            var productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(productFromDb);
            _unitOfWork.save();

            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
