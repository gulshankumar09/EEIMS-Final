using EEIMS.Functionalities;
using EEIMS.Models;
using EEIMS.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EEIMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        // GET: Category

        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            
        }
        public ActionResult Index()
        {
            var categories = _categoryRepository.GetCategories();
            return View(categories);
        }

        [HttpGet]
        public ActionResult AddOrUpdateCategory(int id =0)
        {
            if (id != 0)
            {
                var category = _categoryRepository.GetCategoryById(id);
                return View(category);
            }
            return View(new AddCategoryViewModel());
        }

        [HttpPost]
        public ActionResult AddOrUpdateCategory(AddCategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                if(category.CategoryId != 0)
                    _categoryRepository.UpdateCategory(category);
                else
                    _categoryRepository.InsertCategory(category);

                return RedirectToAction("Index", "Category");
            }
            return View(category);
        }

        public ActionResult DeleteCategory(int id)
        {
            _categoryRepository.DeleteCategory(id);
            return RedirectToAction("Index", "Category");
        }
    }
}