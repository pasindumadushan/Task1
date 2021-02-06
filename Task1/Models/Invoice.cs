using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Task1.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            InvoiceItem = new HashSet<InvoiceItem>();
        }

        public int InvoiceNo { get; set; }
        public int? CustomerRefId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int? ReferenceNo { get; set; }
        public string Note { get; set; }
        public decimal? TotalExcl { get; set; }
        public decimal? TotalTax { get; set; }
        public decimal? TotalIncl { get; set; }

        public virtual ICollection<InvoiceItem> InvoiceItem { get; set; }
    }
}
