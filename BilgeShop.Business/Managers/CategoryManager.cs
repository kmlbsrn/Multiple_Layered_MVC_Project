using BilgeShop.Business.Dtos.Category;
using BilgeShop.Business.Services;
using BilgeShop.Data.Entities;
using BilgeShop.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeShop.Business.Managers
{
    public class CategoryManager : ICategoryService
    {

        private readonly IRepository<CategoryEntity> _categoryRepo;
        private readonly IRepository<ProductEntity> _productRepo;

        public CategoryManager(IRepository<CategoryEntity> categoryRepo,IRepository<ProductEntity> productRepo)
        {
            _categoryRepo=categoryRepo;
            _productRepo=productRepo;
        }
        public bool AddCategory(CategoryAddDto categoryAddDto)
        {
            var hasCategory = _categoryRepo.Get(x=>x.Name.ToLower()==categoryAddDto.Name.ToLower());

            if (hasCategory is not null)
            {
                return false;
            }

            var entity = new CategoryEntity() 
            {
                Name = categoryAddDto.Name,
                Description=categoryAddDto.Description,
            };

            _categoryRepo.Add(entity);
            return true;
        }

        public bool DeleteCategory(int id)
        {
          
            var hasProduct = _productRepo.Get(x=>x.CategoryId==id);

            if (hasProduct is not null)
            {
                return false;
            }

            _categoryRepo.Delete(id);
            return true;

        }

        public List<CategoryListDto> GetCategories()
        {
            var categoryEntites= _categoryRepo.GetAll().OrderBy(x=>x.Name);

            var categoryListDtos = categoryEntites.Select(x=> new CategoryListDto()
            {
                Name=x.Name, 
                Description=x.Description,
                Id=x.Id
            }).ToList();

            return categoryListDtos;
        }

        public CategoryUpdateDto GetCategoryById(int id)
        {
            var categoryEntity= _categoryRepo.GetById(id);

            var categoryUpdateDto = new CategoryUpdateDto() 
            {
                Id=categoryEntity.Id,
                Name=categoryEntity.Name,
                Description=categoryEntity.Description
            };

            return categoryUpdateDto;
        }

        public bool UpdateCategory(CategoryUpdateDto categoryUpdateDto)
        {
            var hasCategory = _categoryRepo.Get(x=>x.Name.ToLower()== categoryUpdateDto.Name.ToLower() && x.Id==categoryUpdateDto.Id);

            if (hasCategory is not null)
            {
                return false;
            }

            var entity = _categoryRepo.GetById(categoryUpdateDto.Id);
            entity.Name = categoryUpdateDto.Name;
            entity.Description = categoryUpdateDto.Description;

            _categoryRepo.Update(entity);

            return true;


            
        }
    }
}
