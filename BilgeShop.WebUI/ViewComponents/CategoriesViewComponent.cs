﻿using BilgeShop.Business.Services;
using BilgeShop.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BilgeShop.WebUI.ViewComponents
{
    // CategoriesViewComponent harici bir controller gibi düşünülebilir.

    public class CategoriesViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;


        public CategoriesViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            var categoryListDtos = _categoryService.GetCategories();


            var viewModel = categoryListDtos.Select(x => new CategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();




            return View(viewModel);
        }
    }
}
