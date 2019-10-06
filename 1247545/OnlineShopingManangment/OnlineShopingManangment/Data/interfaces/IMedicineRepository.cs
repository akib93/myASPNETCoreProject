using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShopingManangment.Models;

namespace OnlineShopingManangment.Data.interfaces
{
    interface IMedicineRepository
    {
        IEnumerable<Medicine> Medicines { get; }
        IEnumerable<Medicine> PreferredMedicines { get;}
        Medicine GetMedicineById(int medicineId);
    }
}
