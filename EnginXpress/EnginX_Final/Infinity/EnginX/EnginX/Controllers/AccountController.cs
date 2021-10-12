using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.IO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using EnginX.Models;

namespace EnginX.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        Infinity_DbEntities db = new Infinity_DbEntities();
        private ApplicationDbContext context;
        private ApplicationRoleManager _roleManager;

        public AccountController()
        {
            context = new ApplicationDbContext();

        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;

        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Main", "Home");
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

    
        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.CityID = new SelectList(db.Cities.Include( r => r.Province).Select(c => new { c.cityID, V = c.Name + " - " + c.Province.Name }), "CityID", "V");
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterViewModel model,int cityID)
        {
            if (ModelState.IsValid)
            {
                string Number = model.MobileNumber.Remove(0, 1);
                string cell = "+27" + Number;

                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, PhoneNumber = cell, EmailConfirmed = true ,PhoneNumberConfirmed = true};
                var result = await UserManager.CreateAsync(user, model.Password);
                var result2 = UserManager.AddToRole(user.Id, "Customer");
                if (result.Succeeded & result2.Succeeded)
                {
                    var User = db.AspNetUsers.Where(x => x.Email == model.Email).FirstOrDefault();
                    User newUser = new User();

                    newUser.Username = User.UserName;
                    newUser.Name = model.Name;
                    newUser.Surname = model.Surname;
                    newUser.Email = model.Email;
                    newUser.ContactNumber = cell;
                    newUser.UserRoleID = Convert.ToInt32(db.User_Roles.Where( r => r.UserRoleID == 5 ).First().UserRoleID);
                    db.Users.Add(newUser);
                    await db.SaveChangesAsync();

                    var dbUser = db.Users.Where(x => x.Email == model.Email).FirstOrDefault();
                    Address newAddress = new Address();
                    newAddress.CityID = cityID;
                    newAddress.UserID = dbUser.UserID;
                    newAddress.HouseNumber = model.HouseNumber;
                    newAddress.StreetName = model.StreetName;
                    db.Addresses.Add(newAddress);
                    await db.SaveChangesAsync();

                    var dbAddress = db.Addresses.Where(x => x.UserID == dbUser.UserID).FirstOrDefault();
                    Customer newCustomer = new Customer();
                    newCustomer.UserID = dbUser.UserID;
                    newCustomer.AddressID = dbAddress.AddressID;
                    db.Customers.Add(newCustomer);
                    await db.SaveChangesAsync();


                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    var home = Url.Action("Index", "Home", Request.Url.Scheme);
                    string body = string.Empty;
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/Templates/Main.html")))
                    {
                        body = reader.ReadToEnd();
                    }
                    body = body.Replace("{ConfirmationLink}", callbackUrl);
                    body = body.Replace("{UserName}", model.UserName);
                    body = body.Replace("{home}", home);
                    body = body.Replace("{Password}", model.Password);
                    await UserManager.SendEmailAsync(user.Id, "Confirm your account", body);
                    ViewBag.Link = callbackUrl;
                    return View("DisplayEmail");
                }
                AddErrors(result);
            }
            ViewBag.CityID = new SelectList(db.Cities.Include(r => r.Province).Select(c => new { c.cityID, V = c.Name + " - " + c.Province.Name }), "CityID", "V");
            return View(model);
        }

        public ActionResult DisplayEmail()
        {
            return View();
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                var home = Url.Action("Index", "Home", Request.Url.Scheme);
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(Server.MapPath("~/Templates/NewDetails.html")))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{ConfirmationLink}", callbackUrl);
                body = body.Replace("{UserName}", user.UserName);
                body = body.Replace("{home}", home);
                await UserManager.SendEmailAsync(user.Id, "ForgotPassword", body);
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(Server.MapPath("~/Templates/MainReset.html")))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{UserName}", model.Email);
                body = body.Replace("{Password}", model.Password);
                await UserManager.SendEmailAsync(user.Id, "Password Changed", body);
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

      
      
        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

     

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
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

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
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

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}