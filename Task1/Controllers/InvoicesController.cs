using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task1.Models;

namespace Task1.Controllers
{
    [Route("api/Invoices")]
    [ApiController]
    public class InvoicesController : Controller
    {
        private readonly SalesOrderContext _context;
        Invoice objInvoice;
        Customer objCustomer;

        public InvoicesController(SalesOrderContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetInvoiceId()
        {
            objInvoice = new Invoice();
            objInvoice.InvoiceDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Add(objInvoice);
                await _context.SaveChangesAsync();
            }

            return Json(new { data = objInvoice });
        }

        [HttpPost]
        public async Task<IActionResult> PostInvoice()
        {
            objInvoice = new Invoice();
            objCustomer = new Customer();

            objCustomer.CustomerName = Request.Form["Customers"];
            objCustomer.Address1 = Request.Form["Address1"];
            objCustomer.Address2 = Request.Form["Address2"];
            objCustomer.Address3 = Request.Form["Address3"];
            objCustomer.Suburb = Request.Form["Suburb"];
            objCustomer.State = Request.Form["State"];
            objCustomer.PostalCode = Request.Form["PostCode"];

            var customer = await _context.Customer.FirstOrDefaultAsync(m => m.CustomerName == objCustomer.CustomerName);

            if (customer.CustomerId == 0)
            {
                return NotFound();
            }
            else
            {
                //_context.Update(objCustomer);
                //await _context.SaveChangesAsync();
            }

            objInvoice.InvoiceNo = Int32.Parse(Request.Form["InvoiceNo"]);
            objInvoice.CustomerRefId = customer.CustomerId;
            objInvoice.InvoiceDate = DateTime.Parse(Request.Form["InvoiceDate"]);
            objInvoice.ReferenceNo = Int32.Parse(Request.Form["ReferenceNo"]);
            objInvoice.Note = Request.Form["Note"];
            objInvoice.TotalExcl = decimal.Parse(Request.Form["TotalExcl"]);
            objInvoice.TotalTax = decimal.Parse(Request.Form["TotalTax"]);
            objInvoice.TotalIncl = decimal.Parse(Request.Form["TotalIncl"]);

            if (objInvoice.InvoiceNo == 0)
            {
                return NotFound();
            }

            _context.Update(objInvoice);
            await _context.SaveChangesAsync();

            return Json("test");
        }

        [HttpGet("GetInvoices")]
        public ActionResult GetInvoices()
        {
            return Json(new { data = _context.Invoice.ToList() });
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteInvoice(int id)
        {
            var item = await _context.Invoice.FirstOrDefaultAsync(u => u.InvoiceNo == id);
            if (item == null)
            {
                return Json(new { data = "Fail" });
            }
            _context.Invoice.Remove(item);
            await _context.SaveChangesAsync();

            return Json(new { data = "success" }); 
        }
    }

}
