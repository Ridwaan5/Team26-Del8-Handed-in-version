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
    public class DeliverStatusController : Controller
    {
        private Infinity_DbEntities db = new Infinity_DbEntities();

        // GET: DeliverStatus
        public async Task<ActionResult> Index()
        {
            return View(await db.DeliverStatus.ToListAsync());
        }

        // GET: DeliverStatus/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliverStatu deliverStatu = await db.DeliverStatus.FindAsync(id);
            if (deliverStatu == null)
            {
                return HttpNotFound();
            }
            return View(deliverStatu);
        }

        // GET: DeliverStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeliverStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DeliveryStatusID,Description")] DeliverStatu deliverStatu)
        {
            if (ModelState.IsValid)
            {
                db.DeliverStatus.Add(deliverStatu);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(deliverStatu);
        }

        // GET: DeliverStatus/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliverStatu deliverStatu = await db.DeliverStatus.FindAsync(id);
            if (deliverStatu == null)
            {
                return HttpNotFound();
            }
            return View(deliverStatu);
        }

        // POST: DeliverStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DeliveryStatusID,Description")] DeliverStatu deliverStatu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deliverStatu).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(deliverStatu);
        }

        // GET: DeliverStatus/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliverStatu deliverStatu = await db.DeliverStatus.FindAsync(id);
            if (deliverStatu == null)
            {
                return HttpNotFound();
            }
            return View(deliverStatu);
        }

        // POST: DeliverStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DeliverStatu deliverStatu = await db.DeliverStatus.FindAsync(id);
            db.DeliverStatus.Remove(deliverStatu);
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
