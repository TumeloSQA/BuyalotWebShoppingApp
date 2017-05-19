using System;
using BuyalotWebShoppingApp.Models;

namespace BuyalotWebShoppingApp.DAL
{
    public class UnitOfWork : IDisposable
    {
        private BuyalotDbContext context = new BuyalotDbContext();
        private GenericRepository<Product> productRepository;
        private GenericRepository<Customer> customerRepository;
        private GenericRepository<ProductCategory> productCategoryRepository;
        private GenericRepository<Admin> adminRepository;

        public GenericRepository<Admin> AdminRepository
        {
            get
            {

                if (this.adminRepository == null)
                {
                    this.adminRepository = new GenericRepository<Admin>(context);
                }
                return adminRepository;
            }
        }

        public GenericRepository<Product> ProductRepository
        {
            get
            {

                if (this.productRepository == null)
                {
                    this.productRepository = new GenericRepository<Product>(context);
                }
                return productRepository;
            }
        }

        public GenericRepository<Customer> CustomerRepository
        {
            get
            {

                if (this.customerRepository == null)
                {
                    this.customerRepository = new GenericRepository<Customer>(context);
                }
                return customerRepository;
            }
        }

        public GenericRepository<ProductCategory> ProductCategoryRepository
        {
            get
            {

                if (this.productCategoryRepository == null)
                {
                    this.productCategoryRepository = new GenericRepository<ProductCategory>(context);
                }
                return productCategoryRepository;
            }
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