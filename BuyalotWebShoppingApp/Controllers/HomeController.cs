using BuyalotWebShoppingApp.Models;
using BuyalotWebShoppingApp.Repository;
using BuyalotWebShoppingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuyalotWebShoppingApp.Controllers
{
    public class HomeController : Controller
    {
        private ICategoryRepository iCategoryRepository = new CategoryRepository();
        private BuyalotDbContext Context { get; set; }
        private bool _DisposeContext = false;

        public HomeController()
        {
            Context = new BuyalotDbContext();
            _DisposeContext = true;
        }


        protected override void Dispose(bool disposing)
        {
            if (_DisposeContext)
                Context.Dispose();

            base.Dispose(disposing);
        }

        public ActionResult Index(string searchString)
        {
            var db = new BuyalotDbContext();

            var product = (from p in db.Products
                           where p.QuantityInStock > 0
                           join pc in db.ProductCategories
                           on p.ProdCategoryID equals pc.ProdCategoryID
                           select new ProductCatViewModel
                           {
                               productImage = p.ProductImage,
                               productID = p.ProductID,
                               price = p.Price,
                               productName = p.ProductName,
                               vendor = p.Vendor,
                               quantityInStock = p.QuantityInStock,
                               productDescription = p.ProductDescription,
                               categoryName = pc.CategoryName
                           });


            if (!String.IsNullOrEmpty(searchString))
            {
                product = product.Where(s => s.productName.StartsWith(searchString)
                                       || s.productName.Contains(searchString)
                                       || s.vendor.StartsWith(searchString)
                                       || s.productDescription.StartsWith(searchString)
                                       || s.productDescription.Contains(searchString)
                                       || s.vendor.Contains(searchString)
                                       || s.categoryName.Contains(searchString)
                                       || s.categoryName.StartsWith(searchString)
                                       );

            }

            ViewBag.ProductList = db.Products.ToList();
            return View(product);
        }

        public ActionResult Category(string id, string searchString)
        {
            //var category = iCategoryRepository.find(id);
            //ViewBag.category = category;


            if (string.IsNullOrEmpty(searchString))
            {
                var category = (from p in Context.Products
                                where p.QuantityInStock > 0
                                join pc in Context.ProductCategories
                                on p.ProdCategoryID equals pc.ProdCategoryID
                                where pc.CategoryName == id
                                select p).ToList();
                ViewBag.products = category;
            }
            else
            {

                var category = (from p in Context.Products
                                where p.QuantityInStock > 0
                                join pc in Context.ProductCategories
                               on p.ProdCategoryID equals pc.ProdCategoryID
                                select new ProductCatViewModel
                                {
                                    productImage = p.ProductImage,
                                    productID = p.ProductID,
                                    price = p.Price,
                                    productName = p.ProductName,
                                    vendor = p.Vendor,
                                    quantityInStock = p.QuantityInStock,
                                    productDescription = p.ProductDescription,
                                    categoryName = pc.CategoryName
                                });


                if (!String.IsNullOrEmpty(searchString))
                {
                    category = category.Where(s => s.productName.StartsWith(searchString)
                                           || s.productName.Contains(searchString)
                                           || s.vendor.StartsWith(searchString)
                                           || s.productDescription.StartsWith(searchString)
                                           || s.productDescription.Contains(searchString)
                                           || s.vendor.Contains(searchString)
                                           || s.categoryName.Contains(searchString)
                                           || s.categoryName.StartsWith(searchString)

                                           );

                }
                ViewBag.products = category;
            }

            return View("Category");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}