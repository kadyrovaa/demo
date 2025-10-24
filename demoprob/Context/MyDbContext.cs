using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using demoprob.Entities2;

namespace demoprob.Context;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<material> materials { get; set; }

    public virtual DbSet<material_supplier> material_suppliers { get; set; }

    public virtual DbSet<material_type> material_types { get; set; }

    public virtual DbSet<product> products { get; set; }

    public virtual DbSet<product_material> product_materials { get; set; }

    public virtual DbSet<product_type> product_types { get; set; }

    public virtual DbSet<supplier> suppliers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=mozaika_company;Username=postgres;Password=2502");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<material>(entity =>
        {
            entity.HasKey(e => e.id).HasName("materials_pkey");

            entity.HasIndex(e => e.material_type_id, "idx_materials_type");

            entity.HasIndex(e => e.material_name, "materials_material_name_key").IsUnique();

            entity.Property(e => e.cost_per_unit).HasPrecision(15, 2);
            entity.Property(e => e.current_stock).HasPrecision(15, 4);
            entity.Property(e => e.material_name).HasMaxLength(100);
            entity.Property(e => e.min_stock_level).HasPrecision(15, 4);
            entity.Property(e => e.package_quantity).HasPrecision(15, 4);
            entity.Property(e => e.unit_of_measure).HasMaxLength(20);

            entity.HasOne(d => d.material_type).WithMany(p => p.materials)
                .HasForeignKey(d => d.material_type_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("materials_material_type_id_fkey");
        });

        modelBuilder.Entity<material_supplier>(entity =>
        {
            entity.HasKey(e => e.id).HasName("material_suppliers_pkey");

            entity.HasIndex(e => e.material_id, "idx_materials_suppliers_material");

            entity.HasIndex(e => e.supplier_id, "idx_materials_suppliers_supplier");

            entity.HasIndex(e => new { e.material_id, e.supplier_id }, "material_suppliers_material_id_supplier_id_key").IsUnique();

            entity.HasOne(d => d.material).WithMany(p => p.material_suppliers)
                .HasForeignKey(d => d.material_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("material_suppliers_material_id_fkey");

            entity.HasOne(d => d.supplier).WithMany(p => p.material_suppliers)
                .HasForeignKey(d => d.supplier_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("material_suppliers_supplier_id_fkey");
        });

        modelBuilder.Entity<material_type>(entity =>
        {
            entity.HasKey(e => e.id).HasName("material_types_pkey");

            entity.HasIndex(e => e.type_name, "material_types_type_name_key").IsUnique();

            entity.Property(e => e.defect_percentage).HasPrecision(5, 4);
            entity.Property(e => e.type_name).HasMaxLength(50);
        });

        modelBuilder.Entity<product>(entity =>
        {
            entity.HasKey(e => e.article_number).HasName("products_pkey");

            entity.HasIndex(e => e.product_type_id, "idx_products_type");

            entity.Property(e => e.article_number).HasMaxLength(50);
            entity.Property(e => e.created_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.min_partner_price).HasPrecision(15, 2);
            entity.Property(e => e.product_name).HasMaxLength(200);
            entity.Property(e => e.roll_width).HasPrecision(10, 2);
            entity.Property(e => e.updated_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.product_type).WithMany(p => p.products)
                .HasForeignKey(d => d.product_type_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_product_type_id_fkey");
        });

        modelBuilder.Entity<product_material>(entity =>
        {
            entity.HasKey(e => e.id).HasName("product_materials_pkey");

            entity.HasIndex(e => e.material_id, "idx_product_materials_material");

            entity.HasIndex(e => e.product_article, "idx_product_materials_product");

            entity.HasIndex(e => new { e.product_article, e.material_id }, "product_materials_product_article_material_id_key").IsUnique();

            entity.Property(e => e.material_quantity).HasPrecision(15, 4);
            entity.Property(e => e.product_article).HasMaxLength(50);

            entity.HasOne(d => d.material).WithMany(p => p.product_materials)
                .HasForeignKey(d => d.material_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_materials_material_id_fkey");

            entity.HasOne(d => d.product_articleNavigation).WithMany(p => p.product_materials)
                .HasForeignKey(d => d.product_article)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_materials_product_article_fkey");
        });

        modelBuilder.Entity<product_type>(entity =>
        {
            entity.HasKey(e => e.id).HasName("product_types_pkey");

            entity.HasIndex(e => e.type_name, "product_types_type_name_key").IsUnique();

            entity.Property(e => e.production_coefficient).HasPrecision(10, 4);
            entity.Property(e => e.type_name).HasMaxLength(50);
        });

        modelBuilder.Entity<supplier>(entity =>
        {
            entity.HasKey(e => e.id).HasName("suppliers_pkey");

            entity.HasIndex(e => e.inn, "suppliers_inn_key").IsUnique();

            entity.HasIndex(e => e.supplier_name, "suppliers_supplier_name_key").IsUnique();

            entity.Property(e => e.inn).HasMaxLength(12);
            entity.Property(e => e.supplier_name).HasMaxLength(100);
            entity.Property(e => e.supplier_type).HasMaxLength(10);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
