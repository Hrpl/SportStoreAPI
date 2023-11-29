using Microsoft.EntityFrameworkCore;
using System.Xml;
using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace API
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options){ }

        public DbSet<Order> Order { get; set; } = null!;
        public DbSet<OrderProduct> OrderProduct { get; set; } = null!;
        public DbSet<PickupPoint> PickupPoint { get; set; } = null!;
        public DbSet<Product> Product { get; set; } = null!;
        public DbSet<RelatedProduct> RelatedProduct { get; set; } = null!;
        public DbSet<Role> Role { get; set; } = null!;
        public DbSet<User> User { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=SportStore;Trusted_Connection=True;");
        }
        //void OnModelCreatingPartial(ModelBuilder modelBuilder);
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07395002E3");

                entity.ToTable("Order");

                entity.Property(e => e.DeliveryDate).HasColumnType("date");
                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.HasOne(d => d.PickupPointNavigation).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PickupPoint)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__PickupPoi__14270015");
            });

            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__OrderPro__3214EC0735F4D58A");

                entity.ToTable("OrderProduct");

                entity.HasOne(d => d.Order).WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderProd__Order__36B12243");

                entity.HasOne(d => d.Product).WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderProd__Produ__2A164134");
            });

            modelBuilder.Entity<PickupPoint>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__PickupPo__3214EC07D237A52C");

                entity.ToTable("PickupPoint");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC071AC60B89");

                entity.ToTable("Product");

                entity.HasIndex(e => e.ArticleNumber, "UQ__tmp_ms_x__3C99114223138137").IsUnique();

                entity.Property(e => e.ArticleNumber).HasMaxLength(100);
                entity.Property(e => e.Cost).HasColumnType("decimal(19, 4)");
                entity.Property(e => e.Photo).IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Role__3214EC0745F24E83");

                entity.ToTable("Role");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__User__3214EC07F347A8D5");

                entity.ToTable("User");

                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.Patronymic).HasMaxLength(100);
                entity.Property(e => e.Surname).HasMaxLength(100);

                entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                    .HasForeignKey(d => d.Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User__Role__267ABA7A");
            });

            //OnModelCreatingPartial(modelBuilder);
        }
        
    }
}
