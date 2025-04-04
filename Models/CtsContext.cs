using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CtsDBFirstEF.Models;

public partial class CtsContext : DbContext
{
    public CtsContext()
    {
    }

    public CtsContext(DbContextOptions<CtsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=DESKTOP-7C6F9K9\\SQLEXPRESS;database=cts;integrated security=true;trustservercertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Custid).HasName("PK__Customer__973AFEFE8D01B458");

            entity.ToTable("Customer");

            entity.Property(e => e.Custid).HasColumnName("custid");
            entity.Property(e => e.Custage).HasColumnName("custage");
            entity.Property(e => e.Custloc)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasDefaultValue("Chennai")
                .HasColumnName("custloc");
            entity.Property(e => e.Custname)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("custname");
        });
              

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
