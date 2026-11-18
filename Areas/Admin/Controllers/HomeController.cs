using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _25DH230146_MyStore.Models;
using _25DH230146_MyStore.Models.ViewModel;

namespace _25DH230146_MyStore.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();

        // GET: Admin/Home
        public ActionResult Index()
        {
            var categoryStats = db.Categories.Select(c => new CategoryStatItem
            {
                CategoryName = c.CategoryName,
                ProductCount = c.Products.Count(),
                MinPrice = c.Products.Any() ? c.Products.Min(p => p.ProductPrice) : 0,
                MaxPrice = c.Products.Any() ? c.Products.Max(p => p.ProductPrice) : 0,
                AvgPrice = c.Products.Any() ? c.Products.Average(p => p.ProductPrice) : 0
            }).ToList();

            var model = new StatisticsVM
            {
                TotalProducts = db.Products.Count(),
                TotalCategories = db.Categories.Count(),
                TotalOrders = db.Orders.Count(),
                TotalRevenue = db.Orders.Any() ? db.Orders.Sum(o => o.TotalAmount) : 0,
                CategoryStats = categoryStats
            };

            return View(model);
        }
    }
}