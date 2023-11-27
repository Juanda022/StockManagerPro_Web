using StockManager.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockManagerPro_Web.Controllers
{
    public class GraphicsController : Controller
    {
        DBStockManagerEntities2 graphicDB = new DBStockManagerEntities2();
        // GET: Graphics
        public ActionResult Graphic()
        {
            return View();
        }

        public ActionResult GraphicView()
        {
            ArrayList xValues = new ArrayList();
            ArrayList yValues = new ArrayList();
            var result = graphicDB.Product.ToList();
            result.ToList().ForEach(rs => xValues.Add(rs.Name));
            result.ToList().ForEach(rs => yValues.Add(rs.UnitPrice));
            var graphic = new System.Web.Helpers.Chart(width: 500, height: 600)
            .AddTitle("Ventas")
            .AddSeries(chartType: "Bar", name: "Barras", xValue: xValues, yValues: yValues);
            return File(graphic.GetBytes("png"), "Image/png");
        }
    }
}