using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StockManager.Models;
using StockManagerPro_Web.Permissions;

namespace StockManager.Controllers
{
    public class EdibleMovementsController : Controller
    {
        private DBStockManagerEntities3 db = new DBStockManagerEntities3();
        [ValidateSession]
        // GET: EdibleMovements
        public ActionResult Index()
        {
            var edibleMovements = db.EdibleMovements.Include(e => e.Employee).Include(e => e.Product);
            return View(edibleMovements.ToList());
        }

        // GET: EdibleMovements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EdibleMovements edibleMovements = db.EdibleMovements.Find(id);
            if (edibleMovements == null)
            {
                return HttpNotFound();
            }
            return View(edibleMovements);
        }

        // GET: EdibleMovements/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.Employee, "EmployeeID", "Name");
            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "Name");
            return View();
        }

        // POST: EdibleMovements/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MovementID,ProductID,MovementType,Quantity,DateTime,EmployeeID")] EdibleMovements edibleMovements)
        {
            if (ModelState.IsValid)
            {
                db.EdibleMovements.Add(edibleMovements);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.Employee, "EmployeeID", "Name", edibleMovements.EmployeeID);
            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "Name", edibleMovements.ProductID);
            return View(edibleMovements);
        }

        // GET: EdibleMovements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EdibleMovements edibleMovements = db.EdibleMovements.Find(id);
            if (edibleMovements == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employee, "EmployeeID", "Name", edibleMovements.EmployeeID);
            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "Name", edibleMovements.ProductID);
            return View(edibleMovements);
        }

        // POST: EdibleMovements/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MovementID,ProductID,MovementType,Quantity,DateTime,EmployeeID")] EdibleMovements edibleMovements)
        {
            if (ModelState.IsValid)
            {
                db.Entry(edibleMovements).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.Employee, "EmployeeID", "Name", edibleMovements.EmployeeID);
            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "Name", edibleMovements.ProductID);
            return View(edibleMovements);
        }

        // GET: EdibleMovements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EdibleMovements edibleMovements = db.EdibleMovements.Find(id);
            if (edibleMovements == null)
            {
                return HttpNotFound();
            }
            return View(edibleMovements);
        }

        // POST: EdibleMovements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EdibleMovements edibleMovements = db.EdibleMovements.Find(id);
            db.EdibleMovements.Remove(edibleMovements);
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
