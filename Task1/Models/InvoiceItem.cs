using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Task1.Models
{
    public partial class InvoiceItem
    {
        public int OrderItemId { get; set; }
        public int InvoiceRefNo { get; set; }
        public int ItemRefCode { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Tax { get; set; }
        public decimal ExclAmount { get; set; }
        public decimal InclAmount { get; set; }
        public decimal TaxAmount { get; set; }

        public virtual Invoice InvoiceRefNoNavigation { get; set; }
        public virtual Item ItemRefCodeNavigation { get; set; }
    }
}
