﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StockManager.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DBStockManagerEntities2 : DbContext
    {
        public DBStockManagerEntities2()
            : base("name=DBStockManagerEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<EdibleMovements> EdibleMovements { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<OrderProducts> OrderProducts { get; set; }
        public virtual DbSet<OrderProductsSale> OrderProductsSale { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Purchases> Purchases { get; set; }
        public virtual DbSet<Sales> Sales { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
