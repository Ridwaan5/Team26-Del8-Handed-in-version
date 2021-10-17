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
using System.IO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace EnginX.Controllers
{
    public class CartsController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext context;
        private ApplicationRoleManager _roleManager;
        public CartsController()
        {
            context = new ApplicationDbContext();

        }
        public CartsController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;

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
        private Infinity_DbEntities db = new Infinity_DbEntities();
        private List<Cart_Line> CartItems = new List<Cart_Line>();
        private static List<CartList> cartObjects = new List<CartList>();
        static decimal Total = 0;
        static decimal VatTotal = 0;
        static decimal Gtotal = 0;


        // GET: Carts
        public async Task<ActionResult> Index()
        {
            var Msg = TempData["Message"] as string;
            if (Msg == null || Msg == "")
            {
                ViewBag.Message = "Welcome";
            }
            else
            {
                ViewBag.Message = Msg;
            }
            ViewBag.Customer = "";
            ViewBag.Total = "0.00";
            var cart = await db.Carts.Include(c => c.Customer).Where(x => x.CartID == HomeController.CartID).FirstOrDefaultAsync();
            if(HomeController.CustomerID != 0 )
            {
                var Customer = db.Customers.Include(u => u.User).Where(x => x.CustomerID == HomeController.CustomerID).FirstOrDefault();
                ViewBag.Customer = Customer.User.Name + " " + Customer.User.Surname;
            }
            
            if (HomeController.CartLines != null)
            {
                CartItems.Clear();
                foreach (var item in HomeController.CartLines)
                {
                    var fitem = db.Cart_Line.Include(p => p.Product).Where(i => i.CartLineID == item.CartLineID).First();
                    CartItems.Add(fitem);
                }
                cartObjects.Clear();
                cartObjects = CartItems.OrderBy(a => a.Product.ProductName).GroupBy(a => a.Product.ProductName, (key, items) => new CartList
                {

                    Name = key,
                    ID = items.Select(x => x.ProductID).First(),
                    Image = items.Select(x => x.Product.Image).First(),
                    PtID = items.Select(x => x.Product.ProductTypeID).First(),
                    vatAmount = items.Select(x => x.Product.Product_Type.Vat.VatAmount).First(),
                    Description = items.Select(x => x.Product.Description).First(),
                    Quntity = items.Count(),
                    Price = items.Sum(item => Convert.ToDecimal(item.Product.Price + ((item.Product.Product_Type.Vat.VatAmount / 100) * item.Product.Price)))
                }).ToList();

                ViewBag.Total = CartItems.Sum(price => price.Product.Price + ((price.Product.Product_Type.Vat.VatAmount/100)*price.Product.Price));
                Total = Convert.ToDecimal(CartItems.Sum(price => price.Product.Price + ((price.Product.Product_Type.Vat.VatAmount / 100) * price.Product.Price)));
                VatTotal = Convert.ToDecimal(CartItems.Sum(price => (price.Product.Product_Type.Vat.VatAmount / 100) * price.Product.Price));
                Gtotal = Convert.ToDecimal(CartItems.Sum(price => price.Product.Price + ((price.Product.Product_Type.Vat.VatAmount / 100) * price.Product.Price)));


            }
            return View(cartObjects);
        }

        public async Task<ActionResult> ResetShopItems()
        {
            foreach(var item in HomeController.CartLines)
            {
                var ItemInShop = db.Products.Where(i => i.ProductID == item.ProductID).First();
                ItemInShop.Stock.Quantity++;
                await db.SaveChangesAsync();       
            }
            HomeController.CartLines.Clear();
            TempData["Message"] = "Successfully Cleared Cart";
            return RedirectToAction("Index", "Home");
        }

        public ActionResult PlusQuantity(string Name)
        {
            //Find Item
            var ItemInShop = db.Products.Where(item => item.ProductName == Name).First();

            //check quantity
            if (ItemInShop.Stock.Quantity == 0 || ItemInShop.Stock.Quantity < 0)
            {
                TempData["Message"] = "Unfortunatly the Product is out of stock ...Please check back soon!!!";
                return RedirectToAction("ViewCart", "Home");
            }
            else if (ItemInShop.Stock.Quantity > 0)
            {
                    var ItemInCart = HomeController.CartLines.Find(item => item.Product.ProductName == Name);
                    //change quantity
                     HomeController.CartLines.Add(ItemInCart);

                    //decrease quantity of stock
                    var ItemInStock = db.Stocks.Where(item => item.StockID == ItemInShop.StockID).FirstOrDefault();
                    ItemInStock.Quantity--;

                    db.SaveChangesAsync();
                    TempData["Message"] = "Successfully added to Cart";          
            }
            return RedirectToAction("Index", "Carts");
        }

        public ActionResult MinusQuantity(string Name)
        {
            // Remove item form cart 
            var ItemInCart = HomeController.CartLines.Find(item => item.Product.ProductName == Name);
            var ItemInShop = db.Products.Where(item => item.ProductName == Name).First();

            HomeController.CartLines.Remove(ItemInCart);

            ItemInShop.Stock.Quantity++;

            db.SaveChangesAsync();
            TempData["Message"] = "Successfully removed from Cart";

            //Route back to View Cart
            return RedirectToAction("Index", "Carts");
        }

        public ActionResult Checkout()
        {

            ViewBag.Total = Total;
            ViewBag.VatTotal = VatTotal;
            ViewBag.Grand = Gtotal;
            return View(cartObjects);
        }

        [HttpPost]
        public async Task<JsonResult> SubmitOrderAsync(string Option, string Customer)
        {
            var Status = "";
            try
            {
                Cart thisCart = db.Carts.Where(x => x.CartID == HomeController.CartID).FirstOrDefault();
                Customer customer = db.Customers.Include(u => u.User).Where(c => c.User.ContactNumber == Customer).FirstOrDefault();
                thisCart.CustomerID = customer.CustomerID;
                thisCart.cartTotal = Total;
                db.Entry(thisCart).State = EntityState.Modified;
                db.SaveChanges();

                int i = 2;
                if (Option == "Cash")
                {
                    i = 1;
                }
                Order newOrder = new Order();
                newOrder.Order_Number = "EN" + DateTime.Now.Second + "GX";
                newOrder.OrderStatusID = 1;
                newOrder.CustomerID = customer.CustomerID;
                newOrder.PaymentID = i;
                newOrder.Dateplaced = DateTime.Now;
                newOrder.CartID = HomeController.CartID;
                db.Orders.Add(newOrder);
                db.SaveChanges();


                //email OrderNumber and sms it to

                var use = db.AspNetUsers.Where(c => c.PhoneNumber == customer.User.ContactNumber).FirstOrDefault();
                string body = string.Empty;
                var callbackUrl = Url.Action("Login", "Account");
                using (StreamReader reader = new StreamReader(Server.MapPath("~/Templates/OrderPlaced.html")))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{ConfirmationLink}", callbackUrl);
                body = body.Replace("{UserName}", customer.User.Name);
                await UserManager.SendEmailAsync(use.Id, "Order Placed", body);
                var date = DateTime.Now.ToShortDateString();
                var time = DateTime.Now.ToShortTimeString();
                var message = new IdentityMessage
                {
                    Destination = customer.User.ContactNumber,
                    Body = "Hi " + " "
                           + customer.User.Name
                           + " 😁" + " You Placed and Order on "
                           + date + " at "
                           + time + " Track the Process on your EngenX Portal"
                };
                await UserManager.SmsService.SendAsync(message);
                Status = "True";
            }
            catch (Exception message)
            {
                Status = message.ToString();
            }

            return new JsonResult { Data = new { Status }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


    }
}
