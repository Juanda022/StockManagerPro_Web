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
    public class OrderProductsSalesController : Controller
    {
        private DBStockManagerEntities3 db = new DBStockManagerEntities3();
        [ValidateSession]
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
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        int userId = Convert.ToInt32(Session["userID"]);
                        int lastMovementId = db.EdibleMovements.Max(e => (int?)e.MovementID) ?? 0;
                        db.OrderProductsSale.Add(orderProductsSale);
                        db.SaveChanges();

                        int orderProductId = orderProductsSale.OrderProductsSaleID;

                        // Paso 3: Insertar en la tabla EdibleMovements
                        EdibleMovements edibleMovement = new EdibleMovements
                        {
                            MovementID = lastMovementId + 1,
                            ProductID = orderProductsSale.ProductID,
                            MovementType = "Salida", // Ajusta según tu lógica
                            Quantity = orderProductsSale.Quantity,
                            DateTime = DateTime.Now, // O ajusta según tus necesidades
                            EmployeeID = userId // Asigna el ID del empleado apropiado
                        };
                        db.EdibleMovements.Add(edibleMovement);
                        db.SaveChanges();

                        Product product = db.Product.Find(orderProductsSale.ProductID);

                        if (product != null)
                        {
                            // Actualizar la cantidad en existencia restando la cantidad de salida
                            product.Quantity -= orderProductsSale.Quantity;

                            // Guardar los cambios en la tabla Inventory
                            db.SaveChanges();
                        }

                        dbContextTransaction.Commit();
                        return RedirectToAction("Index");
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
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

        [HttpPost]
        public ActionResult UpdateSales(int saleID, int[] orderProductIds)
        {
            decimal totalAmount = (decimal)db.OrderProductsSale
            .Where(op => orderProductIds.Contains(op.OrderProductsSaleID))
            .Sum(op => op.Quantity * op.UnitPrice);

            // Actualiza la Ssale con SaleID
            var salesToUpdate = db.Sales.SingleOrDefault(p => p.SaleID == saleID);

            if (salesToUpdate != null)
            {
                salesToUpdate.TotalAmount = totalAmount;
                db.SaveChanges();
            }

            // Redirige o realiza alguna acción después de la actualización
            return RedirectToAction("Index", "Sales"); // Puedes cambiar esto según tu lógica
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
