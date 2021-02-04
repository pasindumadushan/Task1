using System;
using System.Collections.Generic;

#nullable disable

namespace Task1.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            InvoiceItems = new HashSet<InvoiceItem>();
        }

        public int InvoiceNo { get; set; }
        public int CustomerRefId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int ReferenceNo { get; set; }
        public string Note { get; set; }
        public decimal TotalExcl { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalIncl { get; set; }

        public virtual Customer CustomerRef { get; set; }
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
