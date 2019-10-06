using OnlineShopingManangment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopingManangment.ViewModels
{
    public class MedicineListViewModel
    {
        public IEnumerable<Medicine> Medicines { get; set; }
        public string CurrentCategory { get; set; }
    }
}
