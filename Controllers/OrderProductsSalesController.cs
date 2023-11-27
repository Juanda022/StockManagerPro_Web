using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StockManager.Models;

namespace StockManager.Controllers
{
    public class OrderProductsSalesController : Controller
    {
        private DBStockManagerEntities db = new DBStockManagerEntities();

        // GET: OrderProductsSales
        public ActionResult Index()
        {
            var orderProductsSale = db.OrderProductsSale.Include(o => o.Product);
            return View(orderProductsSale.ToList());
        }

        // GET: OrderProductsSales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderProductsSale orderProductsSale = db.OrderProductsSale.Find(id);
            if (orderProductsSale == null)
            {
                return HttpNotFound();
            }
            return View(orderProductsSale);
        }

        // GET: OrderProductsSales/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "Name");
            return View();
        }

        // POST: OrderProductsSales/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderProductsSaleID,OrderID,ProductID,Quantity,UnitPrice")] OrderProductsSale orderProductsSale)
        {
            if (ModelState.IsValid)
            {
                db.OrderProductsSale.Add(orderProductsSale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "Name", orderProductsSale.ProductID);
            return View(orderProductsSale);
        }

        // GET: OrderProductsSales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderProductsSale orderProductsSale = db.OrderProductsSale.Find(id);
            if (orderProductsSale == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "Name", orderProductsSale.ProductID);
            return View(orderProductsSale);
        }

        // POST: OrderProductsSales/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderProductsSaleID,OrderID,ProductID,Quantity,UnitPrice")] OrderProductsSale orderProductsSale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderProductsSale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "Name", orderProductsSale.ProductID);
            return View(orderProductsSale);
        }

        // GET: OrderProductsSales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderProductsSale orderProductsSale = db.OrderProductsSale.Find(id);
            if (orderProductsSale == null)
            {
                return HttpNotFound();
            }
            return View(orderProductsSale);
        }

        // POST: OrderProductsSales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderProductsSale orderProductsSale = db.OrderProductsSale.Find(id);
            db.OrderProductsSale.Remove(orderProductsSale);
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
