using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _25DH230146_MyStore.Models;
using _25DH230146_MyStore.Models.ViewModel;
using PagedList;

namespace _25DH230146_MyStore.Controllers
{
    public class HomeController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();

        // GET: Home/Index
        public ActionResult Index(string searchTerm, int? page)
        {
            var model = new HomeProductVM();
            var products = db.Products.AsQueryable();

            // Tìm kiếm sản phẩm dựa trên từ khóa
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model.SearchTerm = searchTerm;
                products = products.Where(p => p.ProductName.Contains(searchTerm) ||
                                              p.ProductDescription.Contains(searchTerm) ||
                                              p.Category.CategoryName.Contains(searchTerm));
            }

            // Đoạn code liên quan tới phân trang
            int pageNumber = page ?? 1;
            int pageSize = model.PageSize;

            // Lấy top 10 sản phẩm bán chạy nhất
            model.FeaturedProducts = products.OrderByDescending(p =>
            p.OrderDetails.Count()).Take(10).ToList();

            // Lấy danh sách sản phẩm mới và phân trang
            model.NewProducts = products.OrderByDescending(p =>
            p.ProductID).ToPagedList(pageNumber, pageSize);

            return View(model);
        }

        // GET: Home/ProductDetails/5
        public ActionResult ProductDetail(int? id, int? quantity, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Product pro = db.Products.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }

            // Lấy tất cả các sản phẩm cùng danh mục (trừ sản phẩm hiện tại)
            var products = db.Products.Where(p =>
            p.CategoryID == pro.CategoryID && p.ProductID != pro.ProductID).AsQueryable();

            ProductDetailsVM model = new ProductDetailsVM();

            // Đoạn code liên quan tới phân trang
            int pageNumber = page ?? 1;
            int pageSize = model.PageSize;

            model.product = pro;
            model.RelatedProducts = products.OrderBy(p => p.ProductID).Take(8).ToList();
            model.TopProducts = products.OrderByDescending(p =>
            p.OrderDetails.Count()).Take(8).ToPagedList(pageNumber, pageSize);

            if (quantity.HasValue)
            {
                model.quantity = quantity.Value;
            }

            return View(model);
        }
    }
}