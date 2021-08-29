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
    public class Product_TypeController : Controller
    {
        private Infinity_DbEntities db = new Infinity_DbEntities();

        // GET: Product_Type
        public async Task<ActionResult> Index()
        {
            return View(await db.Product_Types.ToListAsync());
        }

        // GET: Product_Type/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Type product_Type = await db.Product_Types.FindAsync(id);
            if (product_Type == null)
            {
                return HttpNotFound();
            }
            return View(product_Type);
        }

        // GET: Product_Type/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product_Type/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProductTypeID,Name,Description")] Product_Type product_Type)
        {
            if (ModelState.IsValid)
            {
                db.Product_Types.Add(product_Type);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(product_Type);
        }

        // GET: Product_Type/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Type product_Type = await db.Product_Types.FindAsync(id);
            if (product_Type == null)
            {
                return HttpNotFound();
            }
            return View(product_Type);
        }

        // POST: Product_Type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProductTypeID,Name,Description")] Product_Type product_Type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_Type).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product_Type);
        }

        // GET: Product_Type/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Type product_Type = await db.Product_Types.FindAsync(id);
            if (product_Type == null)
            {
                return HttpNotFound();
            }
            return View(product_Type);
        }

        // POST: Product_Type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product_Type product_Type = await db.Product_Types.FindAsync(id);
            db.Product_Types.Remove(product_Type);
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
