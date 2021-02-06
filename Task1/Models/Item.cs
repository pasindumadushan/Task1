using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Task1.Models
{
    public partial class Item
    {
        public Item()
        {
            InvoiceItem = new HashSet<InvoiceItem>();
        }

        public int ItemCode { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<InvoiceItem> InvoiceItem { get; set; }
    }
}
