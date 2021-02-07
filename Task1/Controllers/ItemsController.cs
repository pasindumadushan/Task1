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
    [Route("api/Items")]
    [ApiController]
    public class ItemsController : Controller
    {
        private readonly SalesOrderContext _context;
        InvoiceItem objInvoiceItem;
        public ItemsController(SalesOrderContext context)
        {
            _context = context;
        }

        // GET: api/Items
        [HttpGet]
        public ActionResult GetItems()
        {
            return Json(new { data = _context.Item.ToList() });
        }


        [HttpGet("GetAmount")]
        public ActionResult GetAmount(string price, string quantity, string tax)
        {
            var exclAmount = float.Parse(price) * float.Parse(quantity);
            var taxAmount = exclAmount * float.Parse(tax) / 100;
            var inclAmount = exclAmount + taxAmount;

            return Json(new { ExclAmount = exclAmount, TaxAmount = taxAmount, InclAmount = inclAmount });
        }
        
    }
}
