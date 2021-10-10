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
                useremail = db.Customers.Where(x => x.CustomerID == 8).FirstOrDefault().User.Email;
                CustomerID = 8;
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
            else
            {


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
                        ItemInCart.Quantity += 1;

                        //decrease quantity of stock
                        var ItemInStock = db.Stocks.Where(item => item.StockID == ItemInShop.StockID).FirstOrDefault();
                        ItemInStock.Quantity--;


                        db.SaveChangesAsync();
                        TempData["Message"] = "Successfully added to Cart";
                    }
                    else
                    {
                        //Get newly created Cart
                        var CartId = db.Carts.Where(r => r.CartID == C).FirstOrDefault();

                        Cart_Line addtocart = new Cart_Line();
                        addtocart.ProductID = ItemInShop.ProductID;
                        addtocart.Quantity = 1;
                        addtocart.CartID = CartId.CartID;
                        CartLines.Add(addtocart);

                        //decrease quantity of stock
                        var ItemInStock = db.Stocks.Where(item => item.StockID == ItemInShop.StockID).FirstOrDefault();
                        ItemInStock.Quantity--;
                        db.SaveChangesAsync();
                        TempData["Message"] = "Successfully added to Cart";
                    }

                }
                TempData["CartCount"] = CartLines.Count();

            }
            return RedirectToAction("Index");
        }

        public ActionResult Terms()
        {
            return View();
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
                else if(User.IsInRole("Cashier"))
                {
                    return RedirectToAction("Cashier");

                }
                else if (User.IsInRole("Driver"))
                {
                    return RedirectToAction("Driver");

                }
                else if (User.IsInRole("StockClerk"))
                {
                    return RedirectToAction("Clerk");
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
            return View();
        }
        public ActionResult Clerk()
        {
            return View();
        }
        public ActionResult Cashier()
        {
            return View();
        }
        public ActionResult Driver()
        {
            return View();
        }
        public ActionResult Customer()
        {
            return RedirectToAction("Index");
        }
    }
}