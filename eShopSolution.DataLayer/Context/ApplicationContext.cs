﻿using eShopSolution.DataLayer.SeedConfiguration;
using eShopSolution.DtoLayer.Model;
using eShopSolution.EntityLayer.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eShopSolution.DataLayer.Context
{
    public class ApplicationContext : IdentityDbContext<AppUser, AppRole, string>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-Q0LVKG1;Initial Catalog=MyManagerStoreNew;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=True;;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.Entity<Product>()
            .HasOne<Brand>(s => s.Brand)
            .WithMany(g => g.Products)
            .HasForeignKey(s => s.BrandID)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
            .HasOne<Category>(s => s.Category)
            .WithMany(g => g.Products)
            .HasForeignKey(s => s.CategoryID)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
             .HasOne<Gender>(s => s.Gender)
             .WithMany(g => g.Products)
             .HasForeignKey(s => s.GenderID)
             .OnDelete(DeleteBehavior.Cascade);

            #region

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Discount)
               .HasColumnType("decimal(10, 2)")
               .HasPrecision(10, 2);

                entity.Property(p => p.PriceIn)
                      .HasColumnType("decimal(10, 2)")
                      .HasPrecision(10, 2);

                entity.Property(p => p.PriceOut)
                      .HasColumnType("decimal(10, 2)")
                      .HasPrecision(10, 2);
            });
            #endregion

            modelBuilder.Entity<CategoryAndBrand>()
            .HasOne<Brand>(s => s.Brand)
            .WithMany(g => g.CategoryAndBrands)
            .HasForeignKey(s => s.BrandID)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CategoryAndBrand>()
             .HasOne<Category>(s => s.Category)
             .WithMany(g => g.CategoryAndBrands)
             .HasForeignKey(s => s.CategoryID)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CategoryAndBrand>()
           .HasIndex(mp => new { mp.BrandID, mp.CategoryID })
           .IsUnique();

            modelBuilder.Entity<ProductColors>()
            .HasOne<Product>(s => s.Product)
            .WithMany(g => g.ProductColors)
            .HasForeignKey(s => s.ProductID)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductColors>()
            .HasOne<ColorCombination>(s => s.ColorCombination)
            .WithMany(g => g.ProductColors)
            .HasForeignKey(s => s.ColorCombinationID)
            .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<ColorCombinationColor>()
            .HasOne<ColorCombination>(s => s.ColorCombination)
            .WithMany(g => g.ColorCombinationColors)
            .HasForeignKey(s => s.ColorCombinationID)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ColorCombinationColor>()
            .HasOne<Colors>(s => s.Colors)
            .WithMany(g => g.ColorCombinationColors)
            .HasForeignKey(s => s.ColorID)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ColorCombinationColor>()
           .HasIndex(mp => new { mp.ColorID, mp.ColorCombinationID })
           .IsUnique();


            modelBuilder.Entity<ProductImages>()
            .HasOne<ProductColors>(s => s.ProductColors)
            .WithMany(g => g.ProductImages)
            .HasForeignKey(s => s.ProductColorID)
            .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<ProductImages>()
            //.Property(p => p.ID)
            //.ValueGeneratedOnAdd();

            modelBuilder.Entity<ProductSizeInventory>()
            .HasOne<ProductColors>(s => s.ProductColor)
            .WithMany(g => g.ProductSizeInventories)
            .HasForeignKey(s => s.ProductColorID)
            .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<ProductSizeInventory>()
            .HasOne<Sizes>(s => s.Size)
            .WithMany(g => g.ProductSizeInventories)
            .HasForeignKey(s => s.SizeID)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductSizeInventory>()
           .HasIndex(mp => new { mp.SizeID, mp.ProductColorID })
           .IsUnique();

            modelBuilder.Entity<Sizes>()
           .HasIndex(mp => new {mp.Name})
           .IsUnique();

            modelBuilder.Entity<Colors>()
           .HasIndex(mp => new { mp.Name })
           .IsUnique();

            modelBuilder.Entity<Gender>()
           .HasIndex(mp => new { mp.Name })
           .IsUnique();

            modelBuilder.Entity<Brand>()
           .HasIndex(mp => new { mp.BrandName })
           .IsUnique();

            modelBuilder.Entity<Category>()
           .HasIndex(mp => new { mp.CategoryName })
           .IsUnique();

            modelBuilder.Entity<ProductImages>()
           .HasIndex(mp => new { mp.ImageURL })
           .IsUnique();

            modelBuilder.Entity<DetailOrder>()
            .HasOne<ProductSizeInventory>(s => s.ProductSizeInventory)
            .WithMany(g => g.DetailOrders)
            .HasForeignKey(s => s.ProductSizeInventoryID);

            modelBuilder.Entity<DetailOrder>()
           .HasOne<Order>(s => s.Order)
           .WithMany(g => g.DetailOrders)
           .HasForeignKey(s => s.OrderID);

            modelBuilder.Entity<Order>()
            .HasOne<PaymentMethod>(s => s.PaymentMethod)
            .WithMany(g => g.Orders)
            .HasForeignKey(s => s.PaymentMethodID);

            modelBuilder.Entity<Order>()
           .HasOne<OrderStatus>(s => s.OrderStatus)
           .WithMany(g => g.Orders)
           .HasForeignKey(s => s.OrderStatusID);

            modelBuilder.Entity<Order>()
          .HasOne<Address>(s => s.Address)
          .WithMany(g => g.Orders)
          .HasForeignKey(s => s.AddressID);

            modelBuilder.Entity<Order>()
           .HasOne<ShippingProvider>(s => s.ShippingProvider)
           .WithMany(g => g.Orders)
           .HasForeignKey(s => s.ShippingProviderID);

            modelBuilder.Entity<Order>()
           .HasOne<AppUser>(s => s.AppUser)
           .WithMany(g => g.Orders)
           .HasForeignKey(s => s.UserID)
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
           .HasOne<Address>(s => s.Address)
           .WithMany(g => g.Orders)
           .HasForeignKey(s => s.AddressID);


            modelBuilder.Entity<ReturnDetail>()
            .HasOne<DetailOrder>(s => s.DetailOrder)
            .WithMany(g => g.ReturnDetails)
            .HasForeignKey(s => s.DetailOrderID);

            modelBuilder.Entity<ReturnDetail>()
           .HasOne<Return>(s => s.Return)
           .WithMany(g => g.ReturnDetails)
           .HasForeignKey(s => s.ReturnID).
           OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Return>()
           .HasOne<AppUser>(s => s.AppUser)
           .WithMany(g => g.Returns)
           .HasForeignKey(s => s.UserID);

            modelBuilder.Entity<Return>()
           .HasOne<Order>(s => s.Order)
           .WithMany(g => g.Returns)
           .HasForeignKey(s => s.OrderID).
           OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<ProductReview>()
           .HasOne<AppUser>(s => s.AppUser)
           .WithMany(g => g.ProductReviews)
           .HasForeignKey(s => s.UserID);


            modelBuilder.Entity<ProductReview>()
           .HasOne<DetailOrder>(s => s.DetailOrder)
           .WithMany(g => g.ProductReviews)
           .HasForeignKey(s => s.DetailOrderID)
           .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<ProductReview>()
           .HasOne<Product>(s => s.Product)
           .WithMany(g => g.ProductReviews)
           .HasForeignKey(s => s.ProductID);


            modelBuilder.Entity<Address>()
            .HasOne<AppUser>(s => s.AppUser)
            .WithMany(g => g.Addresses)
            .HasForeignKey(s => s.UserID);


            modelBuilder.Entity<Comments>()
           .HasOne<AppUser>(s => s.AppUser)
           .WithMany(g => g.Comments)
           .HasForeignKey(s => s.UserID)
            .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Comments>()
           .HasOne<Product>(s => s.Product)
           .WithMany(g => g.Comments)
           .HasForeignKey(s => s.ProductID)
           .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Comments>()
            .HasOne<Comments>(s => s.ParentComment)
            .WithMany(g => g.Replies)
            .HasForeignKey(c => c.ParentCommentID)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserChatRoom>()
            .HasOne<ChatRoom>(s => s.ChatRoom)
            .WithMany(g => g.UserChatRooms)
            .HasForeignKey(c => c.ChatRoomID)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserChatRoom>()
           .HasOne<AppUser>(s => s.User)
           .WithMany(g => g.UserChatRooms)
           .HasForeignKey(c => c.UserID)
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MessageChat>()
            .HasOne<ChatRoom>(s => s.ChatRoom)
            .WithMany(g => g.Messages)
            .HasForeignKey(c => c.ChatRoomID)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MessageChat>()
           .HasOne<AppUser>(s => s.User)
           .WithMany(g => g.Messages)
           .HasForeignKey(c => c.UserID)
           .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<AspNetRoleAccess>()
            .HasOne<AppRole>(s=>s.AppRole)
            .WithMany(g=>g.AspNetRoleAccesses)
            .HasForeignKey(c=> c.RoleID)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AspNetRoleAccess >()
           .HasOne<MenuPermission>(s => s.MenuPermission)
           .WithMany(g => g.AspNetRoleAccesses)
           .HasForeignKey(c => c.MenuPermissionID)
           .OnDelete(DeleteBehavior.Cascade);

           modelBuilder.Entity<AspNetRoleAccess>()
          .HasIndex(mp => new { mp.MenuPermissionID, mp.RoleID })
          .IsUnique();

            modelBuilder.Entity<MenuPermission>()
            .HasOne(pc => pc.AspNetMenu)
            .WithMany(p => p.MenuPermissions)
            .HasForeignKey(pc => pc.MenuID)
            .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<MenuPermission>()
           .HasOne(pc => pc.Permission)
           .WithMany(p => p.MenuPermissions)
           .HasForeignKey(pc => pc.PermissionID)
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MenuPermission>()
           .HasIndex(mp => new { mp.MenuID, mp.PermissionID })
           .IsUnique();

            base.OnModelCreating(modelBuilder);

        }
        public DbSet<Product> products { get; set; }
        public DbSet<Brand> brands { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Sizes> Sizes { get; set; }
        public DbSet<Colors> Colors { get; set; }
        public DbSet<ProductColors> ProductColors { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<ProductSizeInventory> productSizeInventories { get; set; }
        public DbSet<Address> addresses { get; set; }
        public DbSet<Comments> comments { get; set; }
        public DbSet<Return> returns { get; set; }
        public DbSet<ReturnDetail> returnsDetail { get; set; }
        public DbSet<ShippingProvider> shippingProviders { get; set; }
        public DbSet<CategoryAndBrand> categoryAndBrands { get; set; }
        public DbSet<Advertisements> advertisements { get; set; }
        public DbSet<Gender> gender { get; set; }
        public DbSet<PaymentMethod> paymentMethods { get; set; }
        public DbSet<OrderStatus> orderStatuses { get; set; }
        public DbSet<DetailOrder> detailOrders { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public DbSet<ChatRoom> chatRoom { get; set; }
        public DbSet<UserChatRoom> userChatRoom { get; set; }
        public DbSet<MessageChat> message { get; set; }
        public DbSet<InfoPayment> infoPayments { get; set; }
        public DbSet<Permission> permissions { get; set; }
        public DbSet<AspNetMenu> aspNetMenus { get; set; }
        public DbSet<AspNetRoleAccess> AspNetRoleAccesses { get; set;}
        public DbSet<MenuPermission> menuPermissions {  get; set; } 
        public DbSet<ColorCombination> ColorCombinations { get; set; }
        public DbSet<ColorCombinationColor> ColorCombinationColors { get; set; }
    }
}
