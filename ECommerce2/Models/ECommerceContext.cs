﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ECommerce2.Models
{
    public class ECommerceContext: DbContext
    {
        public ECommerceContext(): base("DefaultConnection")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tax> Taxes { get; set; } 
        public DbSet<Category> Categories { get; set; } 
        public DbSet<Product> Products { get; set; }  
        public DbSet<Warehouse> Warehouse { get; set; }  
        public DbSet<Inventory> Inventories { get; set; }  
        public DbSet<Customer> Customers { get; set; }  
        public DbSet<Status> Status { get; set; }  
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }  
        public DbSet<OrderDetailTmp> OrderDetailTmps { get; set; }
        public DbSet<Sale> Sale { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<CompanyCustomer> CompanyCustomers { get; set; }  

    }
}