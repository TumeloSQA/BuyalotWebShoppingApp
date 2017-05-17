using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using BuyalotWebShoppingApp.Models;
using System.Data.Entity;

namespace BuyalotWebShoppingApp.DAL
{
    public class CategoryRepository : ICategoryRepository, IDisposable
    {
        private BuyalotDbContext context;

        public CategoryRepository(BuyalotDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<ProductCategory> GetCategories()
        {
            return context.ProductCategories.ToList();
        }

        public ProductCategory GetCategoryById(int id)
        {
            return context.ProductCategories.Find(id);
        }

        public void InsertCategory(ProductCategory productCategory)
        {
            context.ProductCategories.Add(productCategory);
        }

        public void DeleteCategory(int ProdCategoryID)
        {
            ProductCategory productCategory = context.ProductCategories.Find(ProdCategoryID);
            context.ProductCategories.Remove(productCategory);
        }

        public void UpdateCategory(ProductCategory productCategory)
        {
            context.Entry(productCategory).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}