using System;
using System.Collections.Generic;

#nullable disable

namespace Task1.Models
{
    public partial class Item
    {
        public Item()
        {
            InvoiceItems = new HashSet<InvoiceItem>();
        }

        public int ItemCode { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
