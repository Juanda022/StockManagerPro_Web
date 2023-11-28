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
    public class OrderProductsController : Controller
    {
        private DBStockManagerEntities3 db = new DBStockManagerEntities3();
        [ValidateSession]
        // GET: OrderProducts
        public ActionResult Index()
        {
            var orderProducts = db.OrderProducts.Include(o => o.Product);
            return View(orderProducts.ToList());
        }

        // GET: OrderProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderProducts orderProducts = db.OrderProducts.Find(id);
            if (orderProducts == null)
            {
                return HttpNotFound();
            }
            return View(orderProducts);
        }

        // GET: OrderProducts/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "Name");
            return View();
        }

        // POST: OrderProducts/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderProductsID,ProductID,Quantity,UnitPrice")] OrderProducts orderProducts)
        {
            if (ModelState.IsValid)
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        int userId = Convert.ToInt32(Session["userID"]);
                        int lastMovementId = db.EdibleMovements.Max(e => (int?)e.MovementID) ?? 0;
                        // Paso 1: Insertar en la tabla OrderProducts
                        db.OrderProducts.Add(orderProducts);
                        db.SaveChanges();

                        // Paso 2: Obtener el ID recién insertado
                        int orderProductId = orderProducts.OrderProductsID;

                        // Paso 3: Insertar en la tabla EdibleMovements
                        EdibleMovements edibleMovement = new EdibleMovements
                        {
                            MovementID = lastMovementId + 1,
                            ProductID = orderProducts.ProductID,
                            MovementType = "Entrada", // Ajusta según tu lógica
                            Quantity = orderProducts.Quantity,
                            DateTime = DateTime.Now, // O ajusta según tus necesidades
                            EmployeeID = userId // Asigna el ID del empleado apropiado
                        };
                        db.EdibleMovements.Add(edibleMovement);
                        db.SaveChanges();

                        Product product = db.Product.Find(orderProducts.ProductID);

                        if (product != null)
                        {
                            // Actualizar la cantidad en existencia restando la cantidad de salida
                            product.Quantity += orderProducts.Quantity;

                            // Guardar los cambios en la tabla Inventory
                            db.SaveChanges();
                        }

                        // Confirmar la transacción
                        dbContextTransaction.Commit();

                        return RedirectToAction("Index");
                    }
                    catch (Exception)
                    {
                        // Revertir la transacción en caso de error
                        dbContextTransaction.Rollback();
                        throw; // Puedes manejar el error de otra manera según tus necesidades
                    }
                }
            }

            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "Name", orderProducts.ProductID);
            return View(orderProducts);
        }

        // GET: OrderProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderProducts orderProducts = db.OrderProducts.Find(id);
            if (orderProducts == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "Name", orderProducts.ProductID);
            return View(orderProducts);
        }

        // POST: OrderProducts/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderProductsID,ProductID,Quantity,UnitPrice")] OrderProducts orderProducts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderProducts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "Name", orderProducts.ProductID);
            return View(orderProducts);
        }

        // GET: OrderProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderProducts orderProducts = db.OrderProducts.Find(id);
            if (orderProducts == null)
            {
                return HttpNotFound();
            }
            return View(orderProducts);
        }

        // POST: OrderProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderProducts orderProducts = db.OrderProducts.Find(id);
            db.OrderProducts.Remove(orderProducts);
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
