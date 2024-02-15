using BilgeShop.Business.Dtos.Category;
using BilgeShop.Business.Services;
using BilgeShop.WebUI.Areas.Admin.Models;
using BilgeShop.WebUI.Areas.Admin.Models.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilgeShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;


        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        public IActionResult List()
        {
            var categoryListDtos = _categoryService.GetCategories();

            var viewModel = categoryListDtos.Select(x => new CategoryListViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description?.Length > 20 ? x.Description.Substring(0, 20) + "..." : x.Description
            }).ToList();

            return View(viewModel);
        }

        public IActionResult New()
        {
            return View("Form", new CategoryFormViewModel());
        }

        public IActionResult Update(int id)
        {
            var categoryUpdateDto = _categoryService.GetCategoryById(id);


            var viewModel = new CategoryFormViewModel()
            {
                Id = categoryUpdateDto.Id,
                Name = categoryUpdateDto.Name,
                Description = categoryUpdateDto.Description
            };

            return View("Form", viewModel);
        }


        [HttpPost]
        public IActionResult Save(CategoryFormViewModel formData)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", formData);
            }

            if (formData.Id == 0)
            {
                var categoryAddDto = new CategoryAddDto()
                {
                    Name = formData.Name.Trim(),
                    Description = formData.Description?.Trim()
                };


                var result = _categoryService.AddCategory(categoryAddDto);


                if (result)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    ViewBag.ErrorMessage = "Bu isimde Kategori zaten var";
                    return View("Form", formData);
                }


            }
            else
            {
                var categoryUpdateDto = new CategoryUpdateDto()
                {
                    Id = formData.Id,
                    Name = formData.Name,
                    Description = formData?.Description
                };

                var result = _categoryService.UpdateCategory(categoryUpdateDto);

                if (!result)
                {
                    ViewBag.ErrorMessage = "Bu isimde Kategori zaten var";
                    return View("Form", formData);
                }


                return RedirectToAction("List");
            }



        }

        public IActionResult Delete(int id)
        {
            if (!_categoryService.DeleteCategory(id))
            {
                TempData["CategoryError"] = "Bu Kategoride ürün olduğundan dolayı silme işlemi gerçekleşmiyor";
                return RedirectToAction("List");
            }
            _categoryService.DeleteCategory(id);
            return RedirectToAction("List");
        }
    }
}
