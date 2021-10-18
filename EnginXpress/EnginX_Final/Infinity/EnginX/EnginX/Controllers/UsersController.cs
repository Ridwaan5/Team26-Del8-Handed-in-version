using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EnginX.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.IO;

namespace EnginX.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        ApplicationDbContext context;
        Infinity_DbEntities db = new Infinity_DbEntities();

        public UsersController()
        {
            context = new ApplicationDbContext();
        }

        public UsersController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager)
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
        // GET: Users
        public async Task<ActionResult> Index()
        {
            var users = db.Users.Include(u => u.User_Role);
            return View(await users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.UserRoleID = new SelectList(db.User_Roles, "UserRoleID", "Description");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserID,Username,Name,Surname,Email,ContactNumber,UserRoleID,Datejoined,userimg")] User userr, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                string role = "";
                if(userr.UserRoleID == 1)
                {
                    role = "Admin";
                }
                else if (userr.UserRoleID == 2)
                {
                    role = "Cashier";
                }
                else if (userr.UserRoleID == 3)
                {
                    role = "Customer";
                }
                else if (userr.UserRoleID == 4)
                {
                    role = "Driver";
                }
                else
                {
                    role = "Clerk";
                }

                var user = new ApplicationUser { UserName = userr.Username, Email = userr.Email, PhoneNumber = userr.ContactNumber , EmailConfirmed = true, PhoneNumberConfirmed = true };
                var result = await UserManager.CreateAsync(user, "EngenX2021");
                var result1 = UserManager.AddToRole(user.Id, role);
                await db.SaveChangesAsync();
                if (result.Succeeded && result1.Succeeded)
                {
                    string Imagepath = "~/Content/Pics/" + image.FileName;
                    image.SaveAs(Path.Combine(HttpContext.Server.MapPath("~/Content/Pics/"), image.FileName));
                    userr.userimg = "~/Content/Pics/" + image.FileName;
                    userr.Datejoined = DateTime.Now;   
                    db.Users.Add(userr);
                    await db.SaveChangesAsync();

                    var emp = db.Users.Where(x => x.ContactNumber == userr.ContactNumber).First();
                    Employee employee = new Employee();
                    employee.UserID = emp.UserID;
                    db.Employees.Add(employee);
                    await db.SaveChangesAsync();

                    var home = Url.Action("Index", "Home", Request.Url.Scheme);
                    string body = string.Empty;
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/Templates/Main.html")))
                    {
                        body = reader.ReadToEnd();
                    }
                    body = body.Replace("{ConfirmationLink}", home);
                    body = body.Replace("{UserName}", userr.Username);
                    body = body.Replace("{home}", home);
                    body = body.Replace("{Password}", "EngenX2021");
                    await UserManager.SendEmailAsync(user.Id, "Welcome to EnginX", body);
                    var message = new IdentityMessage
                    {
                        Destination = userr.ContactNumber,
                        Body = "Hi " + " " + userr.Name + " 😁" + " Welcome To EngenX. " + "This number has successfully been linked..."
                    };
                    await UserManager.SmsService.SendAsync(message);
                }
                return RedirectToAction("Index","Employees");
            }
            ViewBag.UserRoleID = new SelectList(db.User_Roles, "UserRoleID", "Description", userr.UserRoleID);
            return View(userr);
        }


















        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserRoleID = new SelectList(db.User_Roles, "UserRoleID", "Description", user.UserRoleID);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserID,Username,Name,Surname,Email,ContactNumber,UserRoleID,Datejoined,userimg")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UserRoleID = new SelectList(db.User_Roles, "UserRoleID", "Description", user.UserRoleID);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            User user = await db.Users.FindAsync(id);
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
