using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Task1.Models
{
    public partial class SalesOrderContext : DbContext
    {
        public SalesOrderContext()
        {
        }

        public SalesOrderContext(DbContextOptions<SalesOrderContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<InvoiceItem> InvoiceItem { get; set; }
        public virtual DbSet<Item> Item { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SalesOrder;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Address2).HasMaxLength(200);

                entity.Property(e => e.Address3).HasMaxLength(200);

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PostalCode).HasMaxLength(50);

                entity.Property(e => e.State).HasMaxLength(50);

                entity.Property(e => e.Suburb).HasMaxLength(50);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.InvoiceNo);

                entity.Property(e => e.InvoiceDate).HasColumnType("smalldatetime");

                entity.Property(e => e.Note).HasMaxLength(50);

                entity.Property(e => e.TotalExcl).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TotalIncl).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TotalTax).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<InvoiceItem>(entity =>
            {
                entity.HasKey(e => e.OrderItemId);

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.ExclAmount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.InclAmount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Note).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tax).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TaxAmount).HasColumnType("numeric(18, 2)");

                entity.HasOne(d => d.InvoiceRefNoNavigation)
                    .WithMany(p => p.InvoiceItem)
                    .HasForeignKey(d => d.InvoiceRefNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceRefNo_InvoiceNo");

                entity.HasOne(d => d.ItemRefCodeNavigation)
                    .WithMany(p => p.InvoiceItem)
                    .HasForeignKey(d => d.ItemRefCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemRefCode_ItemCode");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.ItemCode);

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
