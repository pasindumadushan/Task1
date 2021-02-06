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

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var item = await _context.Item.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, Item item)
        {
            if (id != item.ItemCode)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        
        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Item>> DeleteItem(int id)
        {
            var item = await _context.Item.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Item.Remove(item);
            await _context.SaveChangesAsync();

            return item;
        }

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.ItemCode == id);
        }
    }
}
