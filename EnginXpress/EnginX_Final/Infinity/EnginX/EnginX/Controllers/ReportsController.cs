using EnginX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static EnginX.Models.Charts;

namespace EnginX.Controllers
{
    public class ReportsController : Controller
    {
        private Infinity_DbEntities db = new Infinity_DbEntities();

        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult ProductReport()
        {
            return View();
        }

        public JsonResult ProductBarChartData()
        {
            int Beverages = db.Products.Where(s => s.Product_Category.ProductCategoryID == 2).Count();
            int Food = db.Products.Where(s => s.Product_Category.ProductCategoryID == 3).Count();
            int Snacks = db.Products.Where(s => s.Product_Category.ProductCategoryID == 4).Count();
            int Candy = db.Products.Where(s => s.Product_Category.ProductCategoryID == 5).Count();
            int Essential = db.Products.Where(s => s.Product_Category.ProductCategoryID == 6).Count();
            int Misc = db.Products.Where(s => s.Product_Category.ProductCategoryID == 7).Count();


            Chart _chart = new Chart();
            _chart.labels = new string[] { "Beverages", "Food", "Snacks", "Candy", "Essential", "Misc" };
            _chart.datasets = new List<Datasets>();
            List<Datasets> _dataSet = new List<Datasets>();
            _dataSet.Add(new Datasets()
            {
                label = "Product Type Category",
                data = new int[] { Beverages, Food, Snacks, Candy, Essential, Misc },
                backgroundColor = new string[] { "800000", "#E9967C", "803544", "#E9967C" , "801250", "#E9967C" },
                borderColor = new string[] { "800000", "#E9967C", "803544", "#E9967C", "801250", "#E9967C" },
                borderWidth = "1.8"
            });
            _chart.datasets = _dataSet;
            return Json(_chart, JsonRequestBehavior.AllowGet);

        }
   
    
    
    }
}