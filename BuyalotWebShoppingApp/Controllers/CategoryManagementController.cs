using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuyalotWebShoppingApp.Models;
using BuyalotWebShoppingApp.DAL;

namespace BuyalotWebShoppingApp.Controllers
{
    public class CategoryManagementController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: CategoryManagement

        public ViewResult Index()
        {
            var categories = unitOfWork.ProductCategoryRepository.Get();
            return View(categories.ToList());
        }

        //
        // GET: /Student/Details/5
        public ViewResult Details(int id)
        {
            ProductCategory productCategory = unitOfWork.ProductCategoryRepository.GetByID(id);
            return View(productCategory);
        }

        //
        // GET: /Student/Create

        public ActionResult Create()
        {
            return View();
        }
        //
        // POST: /Student/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryName")]ProductCategory productCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.ProductCategoryRepository.Insert(productCategory);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, contact your system administrator.");
            }
            return View(productCategory);
        }

        //
        // GET: /ProductCategory/Edit/5
        public ActionResult Edit(int id)
        {
            ProductCategory productCategory = unitOfWork.ProductCategoryRepository.GetByID(id);
            return View(productCategory);
        }

        //
        // GET: /ProductCategory/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryName")]ProductCategory productCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.ProductCategoryRepository.Update(productCategory);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, contact your system administrator.");
            }
            return View(productCategory);
        }


        //
        // GET: /ProductCategory/Delete/5
        public ActionResult Delete(int id)
        {
            ProductCategory productCategory = unitOfWork.ProductCategoryRepository.GetByID(id);
            return View(productCategory);
        }

        //
        // POST: /Student/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductCategory productCategory = unitOfWork.ProductCategoryRepository.GetByID(id);
            unitOfWork.ProductCategoryRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}