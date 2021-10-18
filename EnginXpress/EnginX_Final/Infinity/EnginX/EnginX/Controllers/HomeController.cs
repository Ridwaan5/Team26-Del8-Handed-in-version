using EnginX.Models;
using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace EnginX.Controllers
{
    public class HomeController : Controller
    {
        private Infinity_DbEntities db = new Infinity_DbEntities();
        private static string useremail = null;
        public static List<Cart_Line> CartLines = new List<Cart_Line>();
        public static int CartID = 0;
        public static int CustomerID = 0;

        public ActionResult Index()
        {
            var Products = db.Products.Include(r => r.Product_Category).Include(r => r.Product_Type).Include(r => r.Stock).ToList();
            var Msg = TempData["Message"] as string;

            if (Msg == null || Msg == "")
            {
                ViewBag.Message = "Welcome";
            }
            else
            {
                ViewBag.Message = Msg;
            }
            ViewBag.Count = TempData["CartCount"];
            return View(Products);
        }

        public ActionResult Main()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var sysUser = UserManager.GetEmail(user.GetUserId());
                useremail = sysUser;
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Admin");
                }
                else if (User.IsInRole("Cashie"))
                {
                    return RedirectToAction("Employee");

                }
                else if (User.IsInRole("Driver"))
                {
                    return RedirectToAction("Employee");

                }
                else if (User.IsInRole("StockClerk"))
                {
                    return RedirectToAction("Employee");
                }
                else
                {
                    return RedirectToAction("Customer");
                }
            }
            else
            {
                return View();
            }
        }


        public ActionResult Admin()
        {
            DateTime Month = DateTime.Now.AddDays(-30);
            DateTime JustDate = Month.Date;

            //employees
            ViewBag.EmployeesMade = db.Employees.Count();
            ViewBag.EmployeesOnline = db.Employees.Where(x => x.IsCheckedIn).Count();
            
            //order
            ViewBag.OrdersMade = db.Orders.Count();          
            ViewBag.OrdersThisMonth = db.Orders.Where(x => x.Dateplaced > Month).Count();

            //payments
            if (db.Payments.Count() > 0)
            {
                ViewBag.TotalMade = db.Payments.Where(x => x.Payment_Amount != null).Sum(x => Math.Round((double)x.Payment_Amount, 2));

            }
            else
            {
                ViewBag.TotalMade = 0;
            }
            ViewBag.TotalMadeThisMonth = db.Payments.Where(y =>  y.isPaid == false).Count();

            //Customers
            ViewBag.CustomersMade = db.Customers.Count();
            ViewBag.CustomersThisMonth = db.Customers.Include(u=> u.User).Where(x => x.User.Datejoined > Month).Count();

            var list = db.Orders.Include(os => os.Order_Status)
                                 .Include(cs => cs.Customer)
                                 .Include(pt => pt.Payment)
                                 .Include(ct => ct.Cart)
                                 .Include(em => em.Employee)
                                 .OrderByDescending(x=> x.OrderID)
                                 .Take(10)
                                 .ToList();


            return View(list);
        }
        public ActionResult Employee()
        {
            return View();
        }
        public ActionResult Customer()
        {
            return RedirectToAction("Index");
        }


        [HttpPost]
        public JsonResult searchProduct(string prefix)
        {
            Infinity_DbEntities dbs = new Infinity_DbEntities();
            var product = dbs.Products.Where(
            x => x.ProductName.Contains(prefix) ||
            x.Product_Category.Name.Contains(prefix) ||
            x.Product_Type.Name.Contains(prefix)
            ).ToList().Take(5).Select(x => new { label = x.ProductName, val = x.ProductID });
            return Json(product);

        }

        [HttpGet]
        public JsonResult checkLoggedIn(string prefix)
        {
            var Customer = new object();
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var sysUser = UserManager.GetEmail(user.GetUserId()); 
                List<Customer> foundCustomers = new List<Customer>();
                Customer foundCustomer = new Customer();
                using (Infinity_DbEntities dbs = new Infinity_DbEntities())
                {
                     Customer = (from cs in db.Customers
                                    join us in db.Users on cs.UserID equals us.UserID
                                    join ad in db.Addresses on cs.AddressID equals ad.AddressID
                                    where us.Email == sysUser
                                    select new
                                    {
                                        us.Name,
                                        us.ContactNumber,
                                        ad.HouseNumber,
                                        ad.StreetName,
                                        us.Email
                                    }).FirstOrDefault();
                    //db.Configuration.ProxyCreationEnabled = false;         
                    return new JsonResult { Data = new { Customer }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
            return new JsonResult { Data = new { Customer  }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        [HttpPost]
        public JsonResult searchCustomer(string prefix)
        {
            List<Customer> foundCustomers = new List<Customer>();
            Customer foundCustomer = new Customer();
            using (Infinity_DbEntities dbs = new Infinity_DbEntities())
            {
                var Customer = (from cs in db.Customers
                                join us in db.Users on cs.UserID equals us.UserID
                                join ad in db.Addresses on cs.AddressID equals ad.AddressID
                                where cs.Idnumber == prefix
                                select new
                                {
                                    us.Name,
                                    us.ContactNumber,
                                    ad.HouseNumber,
                                    ad.StreetName,
                                    us.Email
                                 }).FirstOrDefault();
                //db.Configuration.ProxyCreationEnabled = false;         
                return new JsonResult { Data = new { Customer }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

        }

        public async Task<ActionResult> AddToCart(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var Customer = db.Customers.Where(x => x.User.Email == useremail).FirstOrDefault();
                useremail = Customer.User.Email;
                CustomerID = Customer.CustomerID;
            }
            else
            {
                useremail = db.Users.Where(x => x.UserID == 1).FirstOrDefault().Email;
                CustomerID = 10;
            }

            if (CartID == 0)
            {
                /////////////////////////////////////
                var CreatedAt = DateTime.Now;
                Cart newCart = new Cart();
                newCart.CustomerID = CustomerID;
                newCart.CreatedAt = CreatedAt;
                db.Carts.Add(newCart);

                ///////////////////////////////////
                await db.SaveChangesAsync();
                CartID = newCart.CartID;
            }
                //Locate Specific item in Db
                var ItemInShop = db.Products.Where(item => item.ProductID == id).FirstOrDefault();
                //check the quantity of that item and prompt user if item is out of stock
                if (ItemInShop.Stock.Quantity == 0 || ItemInShop.Stock.Quantity < 0)
                {
                    //if items quantity is low/ out of stock the let user know and redirect message to index 
                    TempData["Message"] = "Unfortunatly the Product is out of stock ...Please checkback soon!!!";
                    return RedirectToAction("Index");
                }
                else if (ItemInShop.Stock.Quantity > 0)
                {
                    //find item in cart line 
                    var ItemInCart = CartLines.Find(item => item.ProductID == id);

                    if (ItemInCart != null)
                    {
                        //change quantity
                          CartLines.Add(ItemInCart);

                        //decrease quantity of stock
                        var ItemInStock = db.Stocks.Where(item => item.StockID == ItemInShop.StockID).FirstOrDefault();
                        ItemInStock.Quantity--;


                        db.SaveChangesAsync();
                        TempData["Message"] = "Successfully added to Cart";
                    }
                    else
                    {
                        Cart_Line addtocart = new Cart_Line();
                        addtocart.ProductID = ItemInShop.ProductID;
                        addtocart.Quantity = 1;
                        addtocart.CartID = CartID;
                        db.Cart_Line.Add(addtocart);
                        await db.SaveChangesAsync();
                        addtocart.CartLineID = addtocart.CartLineID;
                        CartLines.Add(addtocart);

                    //decrease quantity of stock
                    var ItemInStock = db.Stocks.Where(item => item.StockID == ItemInShop.StockID).FirstOrDefault();
                        ItemInStock.Quantity--;

                        db.SaveChangesAsync();
                        TempData["Message"] = "Successfully added to Cart";
                    }

                }
                TempData["CartCount"] = CartLines.Count();

            return RedirectToAction("Index");
        }

        public ActionResult Terms()
        {
            return View();
        }

   
 
    }
}