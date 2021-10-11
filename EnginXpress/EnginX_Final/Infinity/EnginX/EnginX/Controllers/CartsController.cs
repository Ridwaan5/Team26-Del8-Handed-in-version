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
        List<Cart_Line> CartItems = new List<Cart_Line>();


        // GET: Carts
        public async Task<ActionResult> Index()
        {
            ViewBag.Customer = "";
            ViewBag.Total = "0.00";
            var cart = await db.Carts.Include(c => c.Customer).Where(x => x.CartID == HomeController.CartID).FirstOrDefaultAsync();
            if(HomeController.CustomerID != 0 )
            {
                var Customer = db.Customers.Include(u => u.User).Where(x => x.CustomerID == HomeController.CustomerID).FirstOrDefault();
                ViewBag.Customer = Customer.User.Name + " " + Customer.User.Surname;
            }
            var cartObjects = new List<CartList>();
            if (HomeController.CartLines != null)
            {
                foreach (var item in HomeController.CartLines)
                {
                    //find in db 
                    var fitem = db.Cart_Line.Include(p => p.Product).Where(i => i.CartLineID == item.CartLineID).First();
                    CartItems.Add(fitem);
                }
                //Linq query to sort and group Occourances of Items in the list 
                cartObjects = CartItems.OrderBy(a => a.Product.ProductName).GroupBy(a => a.Product.ProductName, (key, items) => new CartList
                {
                    Name = key,
                    Quntity = items.Count(),
                    Price = items.Sum(item => Convert.ToInt32(item.Product.Price))
                }).ToList();
                //get totoal using linq sum attribute 
                ViewBag.Total = CartItems.Sum(price => price.Product.Price);

            }
            return View( cartObjects);
        }

        // GET: Carts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = await db.Carts.FindAsync(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // GET: Carts/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerID");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CartID,CustomerID,CreatedAt")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Carts.Add(cart);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerID", cart.CustomerID);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = await db.Carts.FindAsync(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerID", cart.CustomerID);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CartID,CustomerID,CreatedAt")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cart).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerID", cart.CustomerID);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = await db.Carts.FindAsync(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Cart cart = await db.Carts.FindAsync(id);
            db.Carts.Remove(cart);
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
