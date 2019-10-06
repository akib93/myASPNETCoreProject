using Microsoft.AspNetCore.Mvc;
using OnlineShopingManangment.Data.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShopingManangment.Models;

namespace OnlineShopingManangment.Components
{
    public class CategoryMenu: ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public CategoryMenu(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _context.Categories.OrderBy(p => p.CategoryName);
            return View(categories);
        }
    }
}
