using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShopingManangment.Data.interfaces;
using OnlineShopingManangment.Models;
using OnlineShopingManangment.ViewModels;


namespace OnlineShopingManangment.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ShoppingCart _shoppingCart;
        public ShoppingCartController(ApplicationDbContext context, ShoppingCart shoppingCart)
        {
            _context = context;
            _shoppingCart = shoppingCart;

        }
        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(shoppingCartViewModel);
        }
        public RedirectToActionResult AddToShoppingCart(int medicineId)
        {
            var selectedMedicine = _context.Medicines.FirstOrDefault(p => p.MedicineId == medicineId);
            if (selectedMedicine != null)
            {
                _shoppingCart.AddToCart(selectedMedicine, 1);
            }
            return RedirectToAction("Index");
        }
        public RedirectToActionResult RemoveFromShoppingCart(int medicineId)
        {
            var selectedMedicine = _context.Medicines.FirstOrDefault(p => p.MedicineId == medicineId);
            if (selectedMedicine != null)
            {
                _shoppingCart.RemoveFromCart(selectedMedicine);
            }
            return RedirectToAction("Index");
        }
    }
}