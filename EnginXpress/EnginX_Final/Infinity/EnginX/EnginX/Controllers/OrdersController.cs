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
    public class OrdersController : Controller
    {
        private Infinity_DbEntities db = new Infinity_DbEntities();

        // GET: Orders
        public async Task<ActionResult> Index()
        {
            var orders = db.Orders.Include(o => o.Cart).Include(o => o.Customer).Include(o => o.Employee).Include(o => o.Order_Status).Include(o => o.Payment);
            return View(await orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.CartID = new SelectList(db.Carts, "CartID", "CartID");
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerID");
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "EmployeeID");
            ViewBag.OrderStatusID = new SelectList(db.Order_Status, "OrderStatusID", "Order_Status1");
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "PaymentID");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "OrderID,Order_Number,OrderStatusID,CustomerID,PaymentID,Dateplaced,Dateprocessed,Datecompleted,CartID,EmployeeID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CartID = new SelectList(db.Carts, "CartID", "CartID", order.CartID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerID", order.CustomerID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "EmployeeID", order.EmployeeID);
            ViewBag.OrderStatusID = new SelectList(db.Order_Status, "OrderStatusID", "Order_Status1", order.OrderStatusID);
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "PaymentID", order.PaymentID);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CartID = new SelectList(db.Carts, "CartID", "CartID", order.CartID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerID", order.CustomerID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "EmployeeID", order.EmployeeID);
            ViewBag.OrderStatusID = new SelectList(db.Order_Status, "OrderStatusID", "Order_Status1", order.OrderStatusID);
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "PaymentID", order.PaymentID);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OrderID,Order_Number,OrderStatusID,CustomerID,PaymentID,Dateplaced,Dateprocessed,Datecompleted,CartID,EmployeeID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CartID = new SelectList(db.Carts, "CartID", "CartID", order.CartID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerID", order.CustomerID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "EmployeeID", order.EmployeeID);
            ViewBag.OrderStatusID = new SelectList(db.Order_Status, "OrderStatusID", "Order_Status1", order.OrderStatusID);
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "PaymentID", order.PaymentID);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Order order = await db.Orders.FindAsync(id);
            db.Orders.Remove(order);
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
