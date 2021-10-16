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
    public class DeliveriesController : Controller
    {
        private Infinity_DbEntities db = new Infinity_DbEntities();

        // GET: Deliveries
        public async Task<ActionResult> Index()
        {
            var deliveries = db.Deliveries.Include(d => d.DeliverStatu);
            return View(await deliveries.ToListAsync());
        }

        // GET: Deliveries/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Delivery delivery = await db.Deliveries.FindAsync(id);
            if (delivery == null)
            {
                return HttpNotFound();
            }
            return View(delivery);
        }

        // GET: Deliveries/Create
        public ActionResult Create()
        {
            ViewBag.DeliveryStatusID = new SelectList(db.DeliverStatus, "DeliveryStatusID", "Description");
            ViewBag.UserID = new SelectList(db.Employees, "EmployeeID", "EmployeeID");
            return View();
        }

        // POST: Deliveries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DeliveryID,UserID,Description,DeliveryStatusID")] Delivery delivery)
        {
            if (ModelState.IsValid)
            {
                db.Deliveries.Add(delivery);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DeliveryStatusID = new SelectList(db.DeliverStatus, "DeliveryStatusID", "Description", delivery.DeliveryStatusID);
            ViewBag.UserID = new SelectList(db.Employees, "EmployeeID", "EmployeeID", delivery.UserID);
            return View(delivery);
        }

        // GET: Deliveries/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Delivery delivery = await db.Deliveries.FindAsync(id);
            if (delivery == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeliveryStatusID = new SelectList(db.DeliverStatus, "DeliveryStatusID", "Description", delivery.DeliveryStatusID);
            ViewBag.UserID = new SelectList(db.Employees, "EmployeeID", "EmployeeID", delivery.UserID);
            return View(delivery);
        }

        // POST: Deliveries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DeliveryID,UserID,Description,DeliveryStatusID")] Delivery delivery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(delivery).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DeliveryStatusID = new SelectList(db.DeliverStatus, "DeliveryStatusID", "Description", delivery.DeliveryStatusID);
            ViewBag.UserID = new SelectList(db.Employees, "EmployeeID", "EmployeeID", delivery.UserID);
            return View(delivery);
        }

        // GET: Deliveries/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Delivery delivery = await db.Deliveries.FindAsync(id);
            if (delivery == null)
            {
                return HttpNotFound();
            }
            return View(delivery);
        }

        // POST: Deliveries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Delivery delivery = await db.Deliveries.FindAsync(id);
            db.Deliveries.Remove(delivery);
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
