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

        // GET: api/InvoiceItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceItem>>> GetInvoiceItems()
        {
            return await _context.InvoiceItem.ToListAsync();
        }

        // GET: api/InvoiceItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceItem>> GetInvoiceItem(int id)
        {
            var invoiceItem = await _context.InvoiceItem.FindAsync(id);

            if (invoiceItem == null)
            {
                return NotFound();
            }

            return invoiceItem;
        }

        // PUT: api/InvoiceItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoiceItem(int id, InvoiceItem invoiceItem)
        {
            if (id != invoiceItem.OrderItemId)
            {
                return BadRequest();
            }

            _context.Entry(invoiceItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/InvoiceItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        

        // DELETE: api/InvoiceItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<InvoiceItem>> DeleteInvoiceItem(int id)
        {
            var invoiceItem = await _context.InvoiceItem.FindAsync(id);
            if (invoiceItem == null)
            {
                return NotFound();
            }

            _context.InvoiceItem.Remove(invoiceItem);
            await _context.SaveChangesAsync();

            return invoiceItem;
        }

        private bool InvoiceItemExists(int id)
        {
            return _context.InvoiceItem.Any(e => e.OrderItemId == id);
        }
    }
}
