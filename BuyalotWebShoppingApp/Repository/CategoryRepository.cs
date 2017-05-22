using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BuyalotWebShoppingApp.Models;

namespace BuyalotWebShoppingApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private BuyalotDbContext modelDbEntities = new BuyalotDbContext();

        public ProductCategory find(int id)
        {
            return modelDbEntities.ProductCategories.Find(id);
        }

        public List<ProductCategory> findAll()
        {
            return modelDbEntities.ProductCategories.ToList();
        }
    }
}