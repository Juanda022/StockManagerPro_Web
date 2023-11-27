using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Microsoft.Reporting.WebForms;
using StockManager.Models;
//using StockManager.Models.Permissions;


namespace StockManagerPro_Web.Controllers
{
    public class ReporteVentasController : Controller
    {
        /*DBStockManagerEntities2 dbdatos = new DBStockManagerEntities2();
        public ActionResult VistaVentas(DateTime? fechaInicio, DateTime? fechaFin)
        {
            LocalReport reporte = new LocalReport();
            IQueryable<Sales> ventas = dbdatos.Sales.AsQueryable();

            if (fechaInicio.HasValue)
            {
                ventas = ventas.Where(v => v.SaleDate >= fechaInicio.Value);
            }

            if (fechaFin.HasValue)
            {
                ventas = ventas.Where(v => v.SaleDate <= fechaFin.Value);
            }
            return View(ventas.ToList());
        }

        public ActionResult VistaReporteClientes(string id, DateTime? fechaInicio, DateTime? fechaFin)
        {
            LocalReport reporte = new LocalReport();
            string ruta = Path.Combine(Server.MapPath("~/Informes"),
           "ReporteVentas.rdlc");
            reporte.ReportPath = ruta;
            List<Sales> listado = new List<Sales>();
            listado = dbdatos.Sales.ToList();
            ReportDataSource verreporte = new ReportDataSource("Reporte_de_Ventas",
           listado);
            reporte.DataSources.Add(verreporte);
            string tiporeporte = id;
            string mime, codificacion, archivo;
            string[] flujo;
            Warning[] aviso;
            string dispositivo = @"<DeviceInfo>" +
            " <OutputFormat>" + id + "</OutputFormat>" +
            " <PageWidth>8.5in</PageWidth>" +
            " <PageHeight>11in</PageHeight>" +
            " <MarginTop>0.5in</MarginTop>" +
            " <MarginLeft>1in</MarginLeft>" +
            " <MarginRight>1in</MarginRight>" +
            " <MarginBottom>0.5in</MarginBottom>" +
            " <EmbedFonts>None</EmbedFonts>" +
            "</DeviceInfo>";
            byte[] enviar = reporte.Render(id, dispositivo, out mime,
           out codificacion, out archivo, out flujo, out aviso);
            return File(enviar, mime);
        }*/
    }
}