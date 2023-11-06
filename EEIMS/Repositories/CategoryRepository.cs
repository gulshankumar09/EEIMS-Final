using EEIMS.Functionalities;
using EEIMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace EEIMS.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private ApplicationDbContext _context;

        public CategoryRepository()
        {

        }

        public CategoryRepository(ApplicationDbContext applicationDbContext)
        {
            Context = applicationDbContext;
        }

        public ApplicationDbContext Context
        {
            get
            {
                return _context ?? new ApplicationDbContext();
            }
            private set
            {
                _context = value;
            }
        }
        int ICategoryRepository.DeleteCategory(int id)
        {
            Context.Categories.Remove(Context.Categories.Where(c => c.CategoryId == id).FirstOrDefault());
            return Context.SaveChanges();
        }

        IEnumerable<AddCategoryViewModel> ICategoryRepository.GetCategories()
        {
            return Context.Categories.Select(c => new AddCategoryViewModel 
            { 
                CategoryId = c.CategoryId, 
                CategoryName = c.CategoryName 
            }).ToList();
        }

        AddCategoryViewModel ICategoryRepository.GetCategoryById(int id)
        {
             var category = Context.Categories.Where(c => c.CategoryId == id).Select(c => new AddCategoryViewModel 
                { 
                    CategoryId = c.CategoryId, 
                    CategoryName = c.CategoryName 
                }).FirstOrDefault();

            return category;
        }

        int ICategoryRepository.InsertCategory(AddCategoryViewModel model)
        {
            var category =new Category 
            { 
                CategoryId = model.CategoryId,
                CategoryName = model.CategoryName 
            };

            Context.Categories.Add(category);
            return Context.SaveChanges();
        }



        int ICategoryRepository.UpdateCategory(AddCategoryViewModel category)
        {
            Context.Categories.FirstOrDefault(c => c.CategoryId == category.CategoryId).CategoryName = category.CategoryName;  
            return Context.SaveChanges();
        }
    }
}