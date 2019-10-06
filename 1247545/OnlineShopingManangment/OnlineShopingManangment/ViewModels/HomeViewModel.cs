using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShopingManangment.Models;

namespace OnlineShopingManangment.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Medicine> PreferredMedicines { get; set; }
    }
}
