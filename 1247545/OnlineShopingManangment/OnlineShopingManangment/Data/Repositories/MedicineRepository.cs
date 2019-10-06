using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShopingManangment.Data.interfaces;
using OnlineShopingManangment.Data.Repositories;
using OnlineShopingManangment.Models;
using Microsoft.EntityFrameworkCore;

namespace OnlineShopingManangment.Data.Repositories
{
    public class MedicineRepository:IMedicineRepository
    {
        private readonly ApplicationDbContext _appDbContext;
        public MedicineRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IEnumerable<Medicine> Medicines => _appDbContext.Medicines.Include(c => c.Category);

        public IEnumerable<Medicine> PreferredMedicines => _appDbContext.Medicines.Where(p => p.IsPreferredMedicine).Include(c => c.Category);


        public Medicine GetMedicineById(int medicineId) => _appDbContext.Medicines.FirstOrDefault(p => p.MedicineId == medicineId);
    }
   
}
