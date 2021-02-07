using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task1.Models;

namespace Task1.Controllers
{
    [Route("api/InvoiceItems")]
    [ApiController]
    public class InvoiceItemsController : Controller
    {
        private readonly SalesOrderContext _context;
        InvoiceItem objInvoiceItem;

        public InvoiceItemsController(SalesOrderContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostItems()
        {
            objInvoiceItem = new InvoiceItem();

            objInvoiceItem.InvoiceRefNo = Int32.Parse(Request.Form["InvoiceNo"]);
            objInvoiceItem.ItemRefCode = Int32.Parse(Request.Form["ItemCode"]);
            objInvoiceItem.Description = Request.Form["Description"];
            objInvoiceItem.Note = Request.Form["Note2"];
            objInvoiceItem.Quantity = Int32.Parse(Request.Form["Quantity"]);
            objInvoiceItem.Price = decimal.Parse(Request.Form["Price"]);
            objInvoiceItem.Tax = decimal.Parse(Request.Form["Tax"]);
            objInvoiceItem.ExclAmount = decimal.Parse(Request.Form["ExclAmount"]);
            objInvoiceItem.TaxAmount = decimal.Parse(Request.Form["TaxAmount"]);
            objInvoiceItem.InclAmount = decimal.Parse(Request.Form["InclAmount"]);

            _context.Add(objInvoiceItem);
            await _context.SaveChangesAsync();

            return Json(new { data = "test" });
        }
    }
}
