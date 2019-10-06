using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShopingManangment.Data.interfaces;
using OnlineShopingManangment.Models;

namespace OnlineShopingManangment.Data.mocks
{
    public class MockCategoryRepository:ICategoryRepository
    {
        public IEnumerable<Category> Categories {

            get
            {
                return new List<Category> {
                    new Category{ CategoryName="Tablet",Description="All Tablet"},
                    new Category{ CategoryName="Syrup",Description="All Syrup"}
                };
            }
        }
    }
}
