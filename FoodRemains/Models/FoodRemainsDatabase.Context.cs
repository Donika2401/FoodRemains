﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FoodRemains.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FoodRemainsEntities : DbContext
    {
        public FoodRemainsEntities()
            : base("name=FoodRemainsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<FAQMaster> FAQMasters { get; set; }
        public virtual DbSet<FeedbackMaster> FeedbackMasters { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OwnerMaster> OwnerMasters { get; set; }
        public virtual DbSet<PostFoodMaster> PostFoodMasters { get; set; }
        public virtual DbSet<RestaurantMaster> RestaurantMasters { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<UserLogDetail> UserLogDetails { get; set; }
    }
}
