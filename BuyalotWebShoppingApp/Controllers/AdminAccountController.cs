using BuyalotWebShoppingApp.Models;
using BuyalotWebShoppingApp.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BuyalotWebShoppingApp.Controllers
{
    public class AdminAccountController : Controller
    {

        private BuyalotDbContext Context { get; set; }
        private bool _DisposeContext = false;


        public AdminAccountController()
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

        public ActionResult Login()
        {
     
                return View();
    
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Admin model, string returnUrl)
        {

            var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors })
                     .ToArray();

            if (model.isValid(model.email, Cipher.Encrypt(model.password)))
            {
                FormsAuthentication.SetAuthCookie(model.email, false);
                var dataItem = (from c in Context.Admins
                                where c.email == model.email
                                select c).ToList();
                foreach (var admin in dataItem)
                {
                    Session["userID"] = admin.adminID;
                    Session["adminName"] = admin.adminName;

                }
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                 && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "CategoryManagement");
                }
                
            }
            else
                ViewBag.err = "Incorrect Email/Password!Try again!";
            //return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Incorrect details");
            return RedirectToAction("Login", "AdminAccount");
        }

        //[Authorize]
        public ActionResult Logout()
        {
            var response = new HttpStatusCodeResult(HttpStatusCode.Created);
            FormsAuthentication.SignOut();

            Session["adminName"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "AdminAccount");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Success()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ForgotPassword(Admin model)
        {

            string tmpPass = Membership.GeneratePassword(10, 4);
            var getPass = (from p in Context.Admins
                           where p.email == model.email
                           select p).ToList();

            string tempPassword = "";
            foreach (var p in getPass)
            {
                tempPassword = Cipher.Decrypt(p.password);
            }
            MailMessage message = new MailMessage();
            message.From = new System.Net.Mail.MailAddress("maremanetp@gmail.com");
            message.To.Add(new System.Net.Mail.MailAddress(model.email));
            message.Subject = "Buyalot Online Shopping : Password Recovery";
            message.Body = string.Format("Hi {0} ,<br /><br />Your password is: {1} .<br /><br />Thank You. <br /> Regards, <br /> Buyalot DevTeam", model.email, tempPassword);
            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential();
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.Send(message);

            return View("Success");
        }
    }
}
