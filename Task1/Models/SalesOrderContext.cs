using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

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

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }
        public virtual DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SalesOrder;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

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

                entity.ToTable("Invoice");

                entity.Property(e => e.InvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(50);

                entity.Property(e => e.TotalExcl).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TotalIncl).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TotalTax).HasColumnType("numeric(18, 2)");

                entity.HasOne(d => d.CustomerRef)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.CustomerRefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerRefId_CustomerId");
            });

            modelBuilder.Entity<InvoiceItem>(entity =>
            {
                entity.HasKey(e => e.OrderItemId);

                entity.ToTable("InvoiceItem");

                entity.Property(e => e.OrderItemId).ValueGeneratedNever();

                entity.Property(e => e.ExclAmount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.InclAmount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Note).HasMaxLength(50);

                entity.Property(e => e.Tax).HasColumnType("numeric(18, 2)");

                entity.HasOne(d => d.InvoiceRefNoNavigation)
                    .WithMany(p => p.InvoiceItems)
                    .HasForeignKey(d => d.InvoiceRefNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceRefNo_InvoiceNo");

                entity.HasOne(d => d.ItemRefCodeNavigation)
                    .WithMany(p => p.InvoiceItems)
                    .HasForeignKey(d => d.ItemRefCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemRefCode_ItemCode");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.ItemCode);

                entity.ToTable("Item");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
