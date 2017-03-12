using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using OnTheRoad.MVC.Models;
using OnTheRoad.MVC.Factories;
using OnTheRoad.Logic.Enums;

namespace OnTheRoad.MVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAuthenticationServiceFactory authenticationServiceFactory;

        public AccountController(IAuthenticationServiceFactory authenticationServiceFactory)
        {
            if (authenticationServiceFactory == null)
            {
                throw new ArgumentNullException("authenticationServiceFactory can not be null!");
            }

            this.authenticationServiceFactory = authenticationServiceFactory;
        }
        
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var owinContext = this.HttpContext.GetOwinContext();
            var loginService = authenticationServiceFactory.GetLoginService(owinContext);

            var loginStatus = loginService.LoginUser(model.Username, model.Password, model.RememberMe);
            switch (loginStatus)
            {
                case LoginStatus.Success:
                    return RedirectToLocal(returnUrl);

                case LoginStatus.LockedOut:
                    return View("Lockout");

                case LoginStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }
        
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var owinContext = this.HttpContext.GetOwinContext();
                var registerService = authenticationServiceFactory.GetRegisterService(owinContext);

                try
                {
                    registerService.CreateUser(model.Username, model.Email, model.Password, model.FirstName, model.LastName);

                    return RedirectToAction("Index", "Home");
                }
                catch (ArgumentException err)
                {
                    ModelState.AddModelError("", err.Message);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Index", "Home");
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}