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

namespace EnginX.Controllers
{
    public class CartsController : Controller
    {
        private Infinity_DbEntities db = new Infinity_DbEntities();
        private List<Cart_Line> CartItems = new List<Cart_Line>();
        private static List<CartList> cartObjects = new List<CartList>();
        static double Total = 0;
        static double VatTotal = 0;
        static double Gtotal = 0;


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
                    Image = items.Select(x => x.Product.Image).First(),
                    Description = items.Select(x => x.Product.Description).First(),
                    Quntity = items.Count(),
                    Price = items.Sum(item => Convert.ToInt32(item.Product.Price))
                }).ToList();
   
                ViewBag.Total = CartItems.Sum(price => price.Product.Price);
                Total = 0;
                Total = Math.Round(Convert.ToDouble(CartItems.Sum(price => price.Product.Price)),2);
                VatTotal = 0;
                VatTotal = Math.Round(0.15*Total,2);
                Gtotal = 0;
                Gtotal = Total + VatTotal;

            }
            return View(cartObjects);
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



    }
}
