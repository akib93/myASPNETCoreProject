using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShopingManangment.Data.interfaces;
using OnlineShopingManangment.ViewModels;
using OnlineShopingManangment.Models;

namespace OnlineShopingManangment.Controllers
{
    public class MedicineDataController : Controller
    {
        private readonly ApplicationDbContext _medicineRepository;

        public MedicineDataController(ApplicationDbContext medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        [HttpGet]
        public IEnumerable<MedicineViewModel> LoadMoreMedicines()
        {
            IEnumerable<Medicine> dbMedicines = null;

            dbMedicines = _medicineRepository.Medicines.OrderBy(p => p.MedicineId).Take(10);

            List<MedicineViewModel> medicines = new List<MedicineViewModel>();

            foreach (var dbDrink in dbMedicines)
            {
                medicines.Add(MapDbDrinkToDrinkViewModel(dbDrink));
            }
            return medicines;
        }

        private MedicineViewModel MapDbDrinkToDrinkViewModel(Medicine dbDrink) => new MedicineViewModel()
        {
            MedicineId = dbDrink.MedicineId,
            Name = dbDrink.Name,
            Price = dbDrink.Price,
            ShortDescription = dbDrink.ShortDescription,
            ImageThumbnailUrl = dbDrink.ImageThumbnailUrl
        };
    }
}