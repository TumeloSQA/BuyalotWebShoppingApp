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
    public class CustomerManagementController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: CustomerManagement
        public ViewResult Index()
        {
            var customers = unitOfWork.CustomerRepository.Get();
            return View(customers.ToList());
        }

        // GET: CustomerManagement/Details/5
        public ViewResult Details(int id)
        {
            Customer customer = unitOfWork.CustomerRepository.GetByID(id);
            return View(customer);
        }

        // GET: CustomerManagement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerManagement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "customerID,FirstName,LastName,Phone,Email,State")]Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.CustomerRepository.Insert(customer);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, contact your system administrator.");
            }
            return View(customer);
        }

        // GET: CustomerManagement/Edit/5
        public ActionResult Edit(int id)
        {
            Customer customer = unitOfWork.CustomerRepository.GetByID(id);
            return View(customer);
        }

        // POST: CustomerManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( [Bind(Include = "customerID,FirstName,LastName,Phone,Email,State")]Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.CustomerRepository.Update(customer);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, contact your system administrator.");
            }
            return View(customer);
        }

        // GET: CustomerManagement/Delete/5
        public ActionResult Delete(int id)
        {
            Customer customer = unitOfWork.CustomerRepository.GetByID(id);
            return View(customer);
        }

        // POST: CustomerManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = unitOfWork.CustomerRepository.GetByID(id);
            unitOfWork.CustomerRepository.Delete(id);
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
