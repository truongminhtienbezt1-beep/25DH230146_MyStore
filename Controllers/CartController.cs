using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _25DH230146_MyStore.Models;
using _25DH230146_MyStore.Models.ViewModel;

namespace _25DH230146_MyStore.Controllers
{
    public class CartController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();

        // Hàm lấy dịch vụ giỏ hàng
        private CartService GetCartService()
        {
            return new CartService(Session);
        }

        // GET: Cart/Index (Hiển thị giỏ hàng)
        public ActionResult Index()
        {
            var cart = GetCartService().GetCart();
            return View(cart);
        }

        // Thêm sản phẩm vào giỏ
        public ActionResult AddToCart(int id, int quantity = 1)
        {
            var product = db.Products.Find(id);
            if (product != null)
            {
                var cart = GetCartService().GetCart();
                cart.AddItem(product.ProductID, product.ProductImage,
                    product.ProductName, product.ProductPrice, quantity,
                    product.Category.CategoryName);
            }
            return RedirectToAction("Index");
        }

        // Xóa sản phẩm khỏi giỏ
        public ActionResult RemoveFromCart(int id)
        {
            var cart = GetCartService().GetCart();
            cart.RemoveItem(id);
            return RedirectToAction("Index");
        }

        // Làm trống giỏ hàng
        public ActionResult ClearCart()
        {
            GetCartService().ClearCart();
            return RedirectToAction("Index");
        }

        // Cập nhật số lượng sản phẩm
        [HttpPost]
        public ActionResult UpdateQuantity(int id, int quantity)
        {
            var cart = GetCartService().GetCart();
            cart.UpdateQuantity(id, quantity);
            return RedirectToAction("Index");
        }
    }
}