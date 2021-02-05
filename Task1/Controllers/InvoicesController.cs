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

        public InvoicesController(SalesOrderContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult PostInvoice()
        {
            System.Diagnostics.Debug.WriteLine("here" + Request.Form["CustomerName"]);


            return Json("test");
        }
    }
}
