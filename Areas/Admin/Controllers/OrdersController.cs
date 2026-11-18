using _25DH230146_MyStore.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace _25DH230146_MyStore.Areas.Admin.Controllers
{
    public class OrdersController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();

        // GET: Admin/Orders
        public ActionResult Index(string customerName, string paymentStatus, DateTime? orderDate, int? page)
        {
            var orders = db.Orders.Include(o => o.Customer).AsQueryable();
            if (!string.IsNullOrEmpty(customerName))
            {
                orders = orders.Where(o => o.Customer.CustomerName.Contains(customerName));
            }
            if (!string.IsNullOrEmpty(paymentStatus))
            {
                orders = orders.Where(o => o.PaymentStatus == paymentStatus);
            }
            if (orderDate.HasValue)
            {
                orders = orders.Where(o => DbFunctions.TruncateTime(o.OrderDate) == DbFunctions.TruncateTime(orderDate.Value));
            }
            ViewBag.CustomerName = customerName;
            ViewBag.PaymentStatus = paymentStatus;
            ViewBag.OrderDate = orderDate.HasValue ? orderDate.Value.ToString("yyyy-MM-dd") : "";
            int pageNumber = page ?? 1;
            int pageSize = 15;
            var pagedOrders = orders.OrderByDescending(o => o.OrderDate).ToPagedList(pageNumber, pageSize);
            return View(pagedOrders);
        }

        // GET: Admin/Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var order = db.Orders
                          .Include(o => o.Customer)
                          .Include(o => o.OrderDetails.Select(od => od.Product))
                          .SingleOrDefault(o => o.OrderID == id);

            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Admin/Orders/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName");
            return View();
        }

        // POST: Admin/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,CustomerID,OrderDate,TotalAmount,PaymentStatus,ShippingAddress,DeliveryMethod,PaymentMethod")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", order.CustomerID);
            return View(order);
        }

        // GET: Admin/Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            return Details(id);
        }

        // POST: Admin/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int OrderID, string PaymentStatus)
        {
            var order = db.Orders.Find(OrderID);
            if (order == null)
            {
                return HttpNotFound();
            }

            order.PaymentStatus = PaymentStatus;
            db.SaveChanges();

            return RedirectToAction("Details", new { id = OrderID });
        }

        // GET: Admin/Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
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
