using System;
using System.Collections.Generic;
using BuyalotWebShoppingApp.Models;

namespace BuyalotWebShoppingApp.DAL
{
    public interface ICategoryRepository : IDisposable
    {
        IEnumerable<ProductCategory> GetCategories();
        ProductCategory GetCategoryById(int ProdCategoryID);
        void InsertCategory(ProductCategory productCategory);
        void DeleteCategory(int ProdCategoryID);
        void UpdateCategory(ProductCategory productCategory);
        void Save();
    }
}


