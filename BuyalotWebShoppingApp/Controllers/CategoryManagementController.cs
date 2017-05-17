using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using BuyalotWebShoppingApp.Models;
using BuyalotWebShoppingApp.DAL;
using PagedList;

namespace BuyalotWebShoppingApp.Controllers
{
    public class CategoryManagementController : Controller
    {
        // GET: CategoryManagement
        private ICategoryRepository categoryRepository;

        public CategoryManagementController()
        {
            this.categoryRepository = new CategoryRepository(new BuyalotDbContext());
        }

        public CategoryManagementController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        //
        // GET: /Student/

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

            var categories = from s in categoryRepository.GetCategories()
                             select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                categories = categories.Where(s => s.CategoryName.ToUpper().Contains(searchString.ToUpper())
                                       );
            }
            switch (sortOrder)
            {
                case "name_desc":
                    categories = categories.OrderByDescending(s => s.CategoryName);
                    break;
                default:  // Name ascending 
                    categories = categories.OrderBy(s => s.CategoryName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(categories.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Student/Details/5

        public ViewResult Details(int id)
        {
            ProductCategory productCategory = categoryRepository.GetCategoryById(id);
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
        public ActionResult Create(
           [Bind(Include = "CategoryName")]
           ProductCategory productCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    categoryRepository.InsertCategory(productCategory);
                    categoryRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return View(categoryRepository);
        }
        //
        // GET: /Student/Edit/5

        public ActionResult Edit(int id)
        {
            ProductCategory productCategory = categoryRepository.GetCategoryById(id);
            return View(productCategory);
        }

        //
        // POST: /Student/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryName")]ProductCategory productCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    categoryRepository.UpdateCategory(productCategory);
                    categoryRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return View(productCategory);
        }

        //
        // GET: /Student/Delete/5
        public ActionResult Delete(bool? saveChangesError = false, int id = 0)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            ProductCategory productCategory = categoryRepository.GetCategoryById(id);
            return View(productCategory);
        }

        //
        // POST: /Student/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                ProductCategory productCategory = categoryRepository.GetCategoryById(id);
                categoryRepository.DeleteCategory(id);
                categoryRepository.Save();
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            categoryRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}