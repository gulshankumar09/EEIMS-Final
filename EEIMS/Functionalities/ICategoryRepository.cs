using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EEIMS.Models;

namespace EEIMS.Functionalities
{
    public interface ICategoryRepository
    {
        IEnumerable<AddCategoryViewModel> GetCategories();
        AddCategoryViewModel GetCategoryById(int id);
        int InsertCategory(AddCategoryViewModel category);
        int DeleteCategory(int id);
        int UpdateCategory(AddCategoryViewModel  category);
     
    }
}
