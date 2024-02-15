using BilgeShop.Business.Dtos.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeShop.Business.Services
{
    public interface ICategoryService
    {
        bool AddCategory(CategoryAddDto categoryAddDto);

        List<CategoryListDto> GetCategories();

        bool DeleteCategory(int id);

        CategoryUpdateDto GetCategoryById(int id);

        bool UpdateCategory(CategoryUpdateDto categoryUpdateDto);
    }
}
