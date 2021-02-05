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

            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerName == objCustomer.CustomerName);

            objInvoice.CustomerRefId = customer.CustomerId;
            objInvoice.InvoiceNo = Int32.Parse(Request.Form["InvoiceNo"]);
            objInvoice.InvoiceDate = DateTime.Parse(Request.Form["InvoiceDate"]);
            objInvoice.ReferenceNo = Int32.Parse(Request.Form["ReferenceNo"]);
            objInvoice.Note = Request.Form["Note"];

            if (objInvoice.InvoiceNo == 0)
            {
                return NotFound();
            }

            _context.Update(objInvoice);
            await _context.SaveChangesAsync();

            
            System.Diagnostics.Debug.WriteLine(objCustomer.CustomerName);
            System.Diagnostics.Debug.WriteLine(objCustomer.Address1);
            System.Diagnostics.Debug.WriteLine(objCustomer.Address2);
            System.Diagnostics.Debug.WriteLine(objCustomer.Address3);
            System.Diagnostics.Debug.WriteLine(objCustomer.Suburb);
            System.Diagnostics.Debug.WriteLine(objCustomer.State);
            System.Diagnostics.Debug.WriteLine(objCustomer.PostalCode);
            System.Diagnostics.Debug.WriteLine(objInvoice.InvoiceNo);
            System.Diagnostics.Debug.WriteLine(objInvoice.InvoiceDate);
            System.Diagnostics.Debug.WriteLine(objInvoice.ReferenceNo);

            return Json("test");
        }
    }
}
