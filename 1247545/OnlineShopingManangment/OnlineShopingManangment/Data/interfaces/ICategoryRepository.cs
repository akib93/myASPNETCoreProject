using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShopingManangment.Models;

namespace OnlineShopingManangment.Data.interfaces
{
    interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
    }
}
