using BilgeShop.Data.Entities;
using BilgeShop.Data.Enums;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeShop.Data.Context
{
    public class BilgeShopContext : DbContext
    {

        private readonly IDataProtector _protector;

        public BilgeShopContext(DbContextOptions<BilgeShopContext> options, IDataProtectionProvider dataProtectionProvider) : base(options)
        {
            _protector = dataProtectionProvider.CreateProtector("security");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserEntity>().HasData(new UserEntity
            {
                Id = 1,
                Email = "admin@admin.com",
                FirstName = "admin",
                LastName = "admin",
                Password = _protector.Protect("admin"),
                UserType = UserTypeEnum.Admin,
                CreatedDate = DateTime.Now,
                IsDeleted = false,

            });

            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.ApplyConfiguration(new CategoryConfiguration());



            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            base.OnModelCreating(modelBuilder);

        }


        public DbSet<ProductEntity> Products => Set<ProductEntity>();

        public DbSet<UserEntity> Users => Set<UserEntity>();

        public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();

    }
}
