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
    //[Authorize]
    public class CustomerAccountController : Controller
    {

        private BuyalotDbContext Context { get; set; }
        private bool _DisposeContext = false;


        public CustomerAccountController()
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
        public ActionResult Login(User model)
        {

            var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors })
                     .ToArray();

            if (model.isValid(model.Email, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.Username, false);

                var dataItem = (from c in Context.Users
                                where c.Email == model.Email
                                select c).ToList();
                foreach (var user in dataItem)
                {
                    Session["UserId"] = user.userId;
                    Session["username"] = user.Username;

                }


                return RedirectToAction("Index", "Products");
            }
            else
                ViewBag.err = "Incorrect Email/Password!Try again!";
            //return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Incorrect details");
            return RedirectToAction("Login", "CustomerAccount");
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(User model)
        {

            var errors = ModelState
              .Where(x => x.Value.Errors.Count > 0)
              .Select(x => new { x.Key, x.Value.Errors })
               .ToArray();

            if (ModelState.IsValid)
            {

                Models.User user = new Models.User();
                user.Username = model.Username;
                user.Email = model.Email;
                user.Password = model.Password;
                user.ConfirmPassword = model.ConfirmPassword;


                Context.Users.Add(user);
                Context.SaveChanges();

                FormsAuthentication.SetAuthCookie(model.Username, false);
                var dataItem = (from c in Context.Users
                                where c.Email == model.Email
                                select c).ToList();
                foreach (var users in dataItem)
                {
                    Session["UserId"] = users.userId;
                    Session["username"] = users.Username;

                }

                return RedirectToAction("Index", "Products");
            }

            return View(model);

        }
        [Authorize]
        public ActionResult Logout()
        {
            var response = new HttpStatusCodeResult(HttpStatusCode.Created);
            FormsAuthentication.SignOut();

            Session["username"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "CustomerAccount");
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
        public ActionResult ForgotPassword(User model)
        {

            string tmpPass = Membership.GeneratePassword(10, 4);
            var getPass = (from p in Context.Users
                           where p.Email == model.Email
                           select p).ToList();

            string tempPassword = "";
            foreach (var p in getPass)
            {
                tempPassword = Cipher.Decrypt(p.Password);
            }
            MailMessage message = new MailMessage();
            message.From = new System.Net.Mail.MailAddress("maremanetp@gmail.com");
            message.To.Add(new System.Net.Mail.MailAddress(model.Email));
            message.Subject = "Buyalot Online Shopping : Password Recovery";
            message.Body = string.Format("Hi {0} ,<br /><br />Your password is: {0} .<br /><br />Thank You. <br /> Regards, <br /> Buyalot DevTeam", model.Username, model.Password);
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
