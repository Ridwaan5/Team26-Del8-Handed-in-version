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
    public class Supplier_OrderController : Controller
    {
        private Infinity_DbEntities db = new Infinity_DbEntities();

        // GET: Supplier_Order
        public async Task<ActionResult> Index()
        {
            var supplier_Order = db.Supplier_Order.Include(s => s.Administrator).Include(s => s.Supplier).Include(s => s.Supplier_Order_Status);
            return View(await supplier_Order.ToListAsync());
        }

        // GET: Supplier_Order/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier_Order supplier_Order = await db.Supplier_Order.FindAsync(id);
            if (supplier_Order == null)
            {
                return HttpNotFound();
            }
            return View(supplier_Order);
        }

        // GET: Supplier_Order/Create
        public ActionResult Create()
        {
            ViewBag.AdminID = new SelectList(db.Administrators, "AdminID", "AdminID");
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName");
            ViewBag.SOStatusID = new SelectList(db.Supplier_Order_Status, "SOStatusID", "SOStatusDescription");
            return View();
        }

        // POST: Supplier_Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SupplierOrderID,SupplierID,AdminID,SOStatusID,SupplierOrderNumber,SupplierOrderDate,SupplierOrderDescription,SupplierOrderQuantity,SupplierOrderNote")] Supplier_Order supplier_Order)
        {
            if (ModelState.IsValid)
            {
                db.Supplier_Order.Add(supplier_Order);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AdminID = new SelectList(db.Administrators, "AdminID", "AdminID", supplier_Order.AdminID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName", supplier_Order.SupplierID);
            ViewBag.SOStatusID = new SelectList(db.Supplier_Order_Status, "SOStatusID", "SOStatusDescription", supplier_Order.SOStatusID);
            return View(supplier_Order);
        }

        // GET: Supplier_Order/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier_Order supplier_Order = await db.Supplier_Order.FindAsync(id);
            if (supplier_Order == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdminID = new SelectList(db.Administrators, "AdminID", "AdminID", supplier_Order.AdminID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName", supplier_Order.SupplierID);
            ViewBag.SOStatusID = new SelectList(db.Supplier_Order_Status, "SOStatusID", "SOStatusDescription", supplier_Order.SOStatusID);
            return View(supplier_Order);
        }

        // POST: Supplier_Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SupplierOrderID,SupplierID,AdminID,SOStatusID,SupplierOrderNumber,SupplierOrderDate,SupplierOrderDescription,SupplierOrderQuantity,SupplierOrderNote")] Supplier_Order supplier_Order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplier_Order).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AdminID = new SelectList(db.Administrators, "AdminID", "AdminID", supplier_Order.AdminID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName", supplier_Order.SupplierID);
            ViewBag.SOStatusID = new SelectList(db.Supplier_Order_Status, "SOStatusID", "SOStatusDescription", supplier_Order.SOStatusID);
            return View(supplier_Order);
        }

        // GET: Supplier_Order/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier_Order supplier_Order = await db.Supplier_Order.FindAsync(id);
            if (supplier_Order == null)
            {
                return HttpNotFound();
            }
            return View(supplier_Order);
        }

        // POST: Supplier_Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Supplier_Order supplier_Order = await db.Supplier_Order.FindAsync(id);
            db.Supplier_Order.Remove(supplier_Order);
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
