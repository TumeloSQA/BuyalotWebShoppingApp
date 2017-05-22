using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuyalotWebShoppingApp.Models;
using BuyalotWebShoppingApp.DAL;
using PagedList;

namespace BuyalotWebShoppingApp.Controllers
{
    [Authorize]
    public class AdminManagerController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: AdminManager
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var admins = unitOfWork.AdminRepository.Get();
            foreach (var item in admins)
            {
                Session["AdminCount"] = admins.Count();
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                admins = admins.Where(s => s.adminName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    admins = admins.OrderByDescending(s => s.adminName);
                    break;
                default:  // Name ascending 
                    admins = admins.OrderBy(s => s.adminName);
                    break;
            }

            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(admins.ToPagedList(pageNumber, pageSize));
        }

        // GET: AdminManager/Details/5
        public ViewResult Details(int id)
        {
            Admin admin = unitOfWork.AdminRepository.GetByID(id);
            return View(admin);
        }

        // GET: AdminManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "adminID, adminName, email, password")]Admin admin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.AdminRepository.Insert(admin);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, contact your system administrator.");
            }
            return View(admin);
        }



        // GET: AdminManager/Edit/5
        public ActionResult Edit(int id)
        {
            Admin admin = unitOfWork.AdminRepository.GetByID(id);
            return View(admin);
        }

        //
        // GET: /ProductCategory/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "adminID, adminName, email, password")]Admin admin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.AdminRepository.Update(admin);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, contact your system administrator.");
            }
            return View(admin);
        }

        // POST: AdminManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = unitOfWork.AdminRepository.GetByID(id);
            unitOfWork.AdminRepository.Delete(id);
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
